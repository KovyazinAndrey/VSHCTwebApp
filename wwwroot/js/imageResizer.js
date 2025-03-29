window.resizeImage = function (imageUrl) {
    console.log('resizeImage function called');
    return new Promise((resolve, reject) => {
        try {
            console.log('Creating new Image object');
            const img = new Image();
            
            img.onload = function () {
                try {
                    console.log(`Original image size: ${img.width}x${img.height}`);
                    
                    // Максимальные размеры для аватара
                    const maxWidth = 800;
                    const maxHeight = 800;
                    
                    let width = img.width;
                    let height = img.height;

                    // Вычисляем новые размеры, сохраняя пропорции
                    if (width > height) {
                        if (width > maxWidth) {
                            height *= maxWidth / width;
                            width = maxWidth;
                        }
                    } else {
                        if (height > maxHeight) {
                            width *= maxHeight / height;
                            height = maxHeight;
                        }
                    }

                    console.log(`New image size will be: ${width}x${height}`);

                    // Создаем canvas для изменения размера
                    console.log('Creating canvas');
                    const canvas = document.createElement('canvas');
                    canvas.width = width;
                    canvas.height = height;

                    // Рисуем изображение с новыми размерами
                    console.log('Drawing image on canvas');
                    const ctx = canvas.getContext('2d');
                    ctx.drawImage(img, 0, 0, width, height);

                    // Конвертируем в JPEG с качеством 0.8
                    console.log('Converting to JPEG');
                    const resizedImageUrl = canvas.toDataURL('image/jpeg', 0.8);
                    console.log('Conversion complete');
                    
                    resolve(resizedImageUrl);
                } catch (error) {
                    console.error('Error processing image:', error);
                    reject(error);
                }
            };

            img.onerror = function(error) {
                console.error('Error loading image:', error);
                reject(error);
            };

            console.log('Setting image source');
            img.src = imageUrl;
        } catch (error) {
            console.error('Error in resizeImage:', error);
            reject(error);
        }
    });
};

window.triggerFileInput = function (element) {
    console.log('triggerFileInput called with element:', element);
    try {
        if (element) {
            console.log('Element exists, trying to click');
            element.click();
            console.log('Click executed');
        } else {
            console.error('Element is null or undefined');
        }
    } catch (error) {
        console.error('Error in triggerFileInput:', error);
    }
}; 