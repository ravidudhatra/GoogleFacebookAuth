// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {
    // Close toast when close button is clicked
    var closeButtons = document.querySelectorAll('.toast .close');
    closeButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            var toast = this.closest('.toast');
            if (toast) {
                toast.classList.remove('show');
            }
        });
    });
});