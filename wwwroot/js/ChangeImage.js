// Verificar integridade da imagem
function handleImageError() {
    document.getElementById('product-image').style.display = 'none';
    document.getElementById('image-error').style.display = 'block';
}

document.getElementById('image-url').addEventListener('input', function () {
    var imageUrl = this.value;
    document.getElementById('product-image').src = imageUrl;
    document.getElementById('product-image').style.display = 'block';
    document.getElementById('image-error').style.display = 'none';
});

// Adiciona um evento de escuta ao campo de entrada de URL da imagem
document.getElementById('image-url').addEventListener('input', function () {
    // Obtém a nova URL da imagem
    var imageUrl = this.value;

    // Atualiza o atributo src da imagem com a nova URL
    document.getElementById('product-image').src = imageUrl;
});

// Carregar Imagem no inicio
window.addEventListener('DOMContentLoaded', function () {
    var imageUrl = document.getElementById('image-url').value;
    document.getElementById('product-image').src = imageUrl;
    document.getElementById('product-image').style.display = 'block';
    document.getElementById('image-error').style.display = 'none';
});