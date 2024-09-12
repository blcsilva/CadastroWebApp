// site.js

// Função para exibir uma mensagem de boas-vindas quando a página é carregada
document.addEventListener('DOMContentLoaded', function () {
    console.log('Página carregada e pronta!');

    // Exemplo de código JavaScript para exibir um alerta ao clicar em um botão
    const button = document.querySelector('button');
    if (button) {
        button.addEventListener('click', function () {
            alert('Botão clicado!');
        });
    }
});
