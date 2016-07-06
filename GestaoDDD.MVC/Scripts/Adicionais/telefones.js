$("#pres_cpf_cnpj").mask("99999999999");
$('#pres_telefone_celular').focusout(function () {
    var phone, element;
    element = $(this);
    element.unmask();
    phone = element.val().replace(/\D/g, '');
    if (phone.length > 10) {
        element.mask("(99)99999-999?9");
    } else {
        element.mask("(99)9999-9999?9");
    }
}).trigger('focusout');
$('#pres_telefone_residencial').focusout(function () {
    var phone, element;
    element = $(this);
    element.unmask();
    phone = element.val().replace(/\D/g, '');
    if (phone.length > 10) {
        element.mask("(99)9999-9999");
    } else {
        element.mask("(99)9999-9999");
    }
}).trigger('focusout');