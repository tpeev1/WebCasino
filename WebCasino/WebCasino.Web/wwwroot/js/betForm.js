$(function () {

    $('#betForm').on('submit', function (event) {
        event.preventDefault();
        const $withdrawForm = $('#betForm');
        const value = +($withdrawForm.find('input[name="BetAmount"]').val());
        const balance = +($('#balanceSpan').html());
        if (value > balance || value < 1) {
            const spanValidation = $withdrawForm.find('*[data-valmsg-for="BetAmount"]');
            spanValidation.addClass('field-validation-error');
            spanValidation.removeClass('field-validation-valid');
            spanValidation.html("Insufficient funds");

        }
        else {

            const data = $(this).serialize();
            const url = $(this).attr("action");
            const posting = $.post(url, data);

            posting.done(function (data) {
                $('#gameBoard').html(data);

                console.log(data);
            });
    }

    });
});