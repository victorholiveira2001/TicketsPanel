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