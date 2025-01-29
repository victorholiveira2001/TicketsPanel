$(document).ready(function () {
    $('#Department').change(function () {
        var departmentId = $(this).val();
        var $categorySelect = $('#Category');

        $categorySelect.empty();
        $categorySelect.append('<option>Selecionar Categoria.</option>');

        if (departmentId) {
            $.ajax({
                url: '/Tickets/GetCategoriesByDepartment',
                type: 'GET',
                dataType: 'json',
                data: { departmentId: departmentId },
                success: function (data) {
                    $.each(data, function (index, category) {
                        $categorySelect.append('<option value="' + category.categoryId + '">' + category.name + '</option>');
                    });
                },
                error: function (xhr, status, error) {
                    console.log("Error on AJAX request: " + error);
                }
            });
        }
    });
});

{
    const fileInput = document.getElementById('file');
    const fileList = document.getElementById('file-list');
    let uploadedFiles = []; // Array para armazenar os arquivos anexados

    fileInput.addEventListener('change', function () {
        const newFiles = Array.from(fileInput.files); // Captura os novos arquivos
        uploadedFiles = [...uploadedFiles, ...newFiles]; // Acumula os arquivos antigos e novos

        fileList.innerHTML = ''; // Limpa a lista na interface para atualizar

        // Exibe todos os arquivos anexados
        uploadedFiles.forEach((file, index) => {
            const listItem = document.createElement('li');
            listItem.textContent = `${file.name}`;
            fileList.appendChild(listItem);
        });

        fileInput.value = ''; // Limpa o input para permitir reanexar
    });

}
