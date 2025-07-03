$(document).ready(function () {
    // Pause scroll on card hover
    $(document).on('mouseenter', '.scroll-card', function () {
        $('.scroll-track').addClass('paused');
    });

    $(document).on('mouseleave', '.scroll-card', function () {
        $('.scroll-track').removeClass('paused');
    });

    // Read More toggle
    $('#toggle-btn').click(function () {
        const moreText = $('#more-text');
        const btn = $(this);

        if (moreText.hasClass('hidden')) {
            moreText.removeClass('hidden');
            btn.text('Read Less');
        } else {
            moreText.addClass('hidden');
            btn.text('Read More');
        }
    });

  

    // Booking date minimum set to today
    const today = new Date().toISOString().split('T')[0];
    $('#booking-date').attr('min', today);

    // Booking date must be Tuesday
    $('#booking-date').on('change', function () {
        const selectedDate = new Date(this.value);
        const isTuesday = selectedDate.getDay() === 2;

        if (!isTuesday) {
            alert("Bookings are only available for Tuesday.");
            this.value = '';
        }
    });

    // Form submission alert
    $('#booking-form').on('submit', function () {
        alert("✅ Booking submitted successfully!");
    });

    // Dark mode toggle
    const toggleBtn = document.getElementById("mode-toggle");
    if (toggleBtn) {
        toggleBtn.addEventListener("click", () => {
            document.body.classList.toggle("dark-mode");
            toggleBtn.textContent = document.body.classList.contains("dark-mode") ? "🐔☀️" : "🐔💤";
        });
    }
});

