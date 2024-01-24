$(document).ready(function () {
  $("input:radio[name='inlineRadioOptions']").on("change", function () {
    if ($(this).val() === "true") {
      $("[name='show']").css({ visibility: "visible" }); // Show the dropdown
    } else {
      $("[name='show']").css({ visibility: "hidden" }); // Hide the dropdown
    }
  });
});

function myFunction1() {
  document.getElementById("show").style.visibility = "hidden";
}
