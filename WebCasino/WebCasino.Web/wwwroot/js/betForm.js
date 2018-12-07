$(function () {
    const $withdrawForm = $('#betForm');

    $withdrawForm.on('submit', function (event) {
        event.preventDefault();

        const value = $withdrawForm.find('input[name="BetAmount"]').val();
        const balance = $('#balanceSpan').html();
        if (value > balance || value < 1) {
            const spanValidation = $withdrawForm.find('*[data-valmsg-for="BetAmount"]');
            spanValidation.addClass('field-validation-error');
            spanValidation.removeClass('field-validation-valid');
            spanValidation.html("Insufficient funds");

        }
        else {
            $(this).off("submit");
            $(this).submit();
        }
    });

});