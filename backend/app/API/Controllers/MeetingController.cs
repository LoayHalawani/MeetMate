using API.Resources;
using API.Validators;
using AutoMapper;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;
        private readonly IMapper _mapper;

        public MeetingController(IMeetingService meetingService, IMapper mapper)
        {
            this._mapper = mapper;
            this._meetingService = meetingService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<MeetingResource>>> GetAllMeetings()
        {
            var meetings = await _meetingService.GetAllMeetings();
            var meetingResources = _mapper.Map<IEnumerable<Meeting>, IEnumerable<MeetingResource>>(meetings);

            return Ok(meetingResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MeetingResource>> GetMeetingById(int id)
        {
            var meeting = await _meetingService.GetMeetingById(id);
            var meetingResource = _mapper.Map<Meeting, MeetingResource>(meeting);

            return Ok(meetingResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<MeetingResource>> CreateMeeting([FromBody] SaveMeetingResource saveMeetingResource)
        {
            var validator = new SaveMeetingResourceValidator();
            var validationResult = await validator.ValidateAsync(saveMeetingResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var meetingToCreate = _mapper.Map<SaveMeetingResource, Meeting>(saveMeetingResource);

            var newMeeting = await _meetingService.CreateMeeting(meetingToCreate);

            var meeting = await _meetingService.GetMeetingById(newMeeting.MeetingId);

            var meetingResource = _mapper.Map<Meeting, MeetingResource>(meeting);

            return Ok(meetingResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MeetingResource>> UpdateMeeting(int id, [FromBody] SaveMeetingResource saveMeetingResource)
        {
            var validator = new SaveMeetingResourceValidator();
            var validationResult = await validator.ValidateAsync(saveMeetingResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var meetingToBeUpdate = await _meetingService.GetMeetingById(id);

            if (meetingToBeUpdate == null)
                return NotFound();

            var meeting = _mapper.Map<SaveMeetingResource, Meeting>(saveMeetingResource);

            await _meetingService.UpdateMeeting(meetingToBeUpdate, meeting);

            var updatedMeeting = await _meetingService.GetMeetingById(id);
            var updatedMeetingResource = _mapper.Map<Meeting, MeetingResource>(updatedMeeting);

            return Ok(updatedMeetingResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeeting(int id)
        {
            if (id == 0)
                return BadRequest();

            var meeting = await _meetingService.GetMeetingById(id);

            if (meeting == null)
                return NotFound();

            await _meetingService.DeleteMeeting(meeting);

            return NoContent();
        }
    }
}
