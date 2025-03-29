window.triggerClick = function (element) {
    if (element) {
        console.log('Triggering click on file input');
        element.click();
    } else {
        console.error('Element is null');
    }
}; 