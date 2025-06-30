$(document).ready(function () {
    $(".course-register").click(function () {
        var courseId = $(this).data("id");
        var userId = $(this).data("userId"); // Note: JavaScript uses camelCase for data attributes
        
        console.log("Sending data:", { courseId, userId }); // Debugging
        
        $.ajax({
            url: "https://localhost:7189/Signal/", // Make sure endpoint is correct
            type: "POST",
            contentType: "application/json", // Add this
            data: JSON.stringify({ // Stringify the data
                courseId: courseId,
                userId: userId
            }),
            success: function (response) {
                alert(response.message);
                window.location.reload();
            },
            error: function(xhr, status, error) {
                console.error("Error:", error);
            }
        });
    });
});