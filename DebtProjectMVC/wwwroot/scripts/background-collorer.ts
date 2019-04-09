$(document).ready(() => {
    let iterator: number = 1;
    $('section.main-container').toArray().forEach(function(value){
        value.classList.add("b-c-" + iterator);
        iterator++;
    })
})