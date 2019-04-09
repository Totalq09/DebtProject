$(document).ready(function () {
    var iterator = 1;
    $('section.main-container').toArray().forEach(function (value) {
        value.classList.add("b-c-" + iterator);
        iterator++;
    });
});
//# sourceMappingURL=background-collorer.js.map