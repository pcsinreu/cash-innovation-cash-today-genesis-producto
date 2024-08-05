function GridExpansivel_Inicializar(idDivRaiz) {
    $(function () {
        // Expansão
        $('#' + idDivRaiz + ' [linhaexpansiva]').each(function () {
            var $linhaExpansora = $(this);
            var $imagem = $linhaExpansora.prev().find('[imagemexpansor]');
            if (!$imagem.data('toggleInicializado')) {
                $imagem.data('toggleInicializado', true);
                var $hidden = $imagem.parent().find(':hidden');
                $imagem.unbind('click').click(function () {
                    if ($linhaExpansora.is(':visible')) {
                        $linhaExpansora.hide();
                        $imagem.attr('src', PATH_IMAGENES + 'Expandir.png');
                        $hidden.val('0');
                    } else {
                        $linhaExpansora.show();
                        $imagem.attr('src', PATH_IMAGENES + 'Retrair.png');
                        $hidden.val('1');
                    };
                });
            }
        });
        // Checks
        var $checkTabela = $('#' + idDivRaiz + ' [checktabela] :checkbox');
        var $todosChecks = $('#' + idDivRaiz + ' [checkexpansor] :checkbox');
        $checkTabela.unbind('change').change(function () {
            $todosChecks.prop('checked', this.checked);
        });
        $todosChecks.unbind('change').change(function () {
            var $check = $(this);
            var $linhasExpansorasPais = $check.parents('[linhaexpansiva]');
            var $linhaExpansora = $check.parents('tr:first').next();
            if ($linhaExpansora.is('[linhaexpansiva]')) {
                $linhaExpansora.find('[checkexpansor] :checkbox').prop('checked', this.checked);
            };
            if (!this.checked) {
                $linhasExpansorasPais.prev().find('[checkexpansor] :checkbox').prop('checked', false);
            }
            $linhasExpansorasPais.each(function () {
                var $linhaExpansora = $(this);
                var $checks = $linhaExpansora.find('[checkexpansor] :checkbox');
                $linhaExpansora.prev().find('[checkexpansor] :checkbox').prop('checked', $checks.length == $checks.filter(':checked').length);
            });
            $checkTabela.prop('checked', $todosChecks.length == $todosChecks.filter(':checked').length);
        });
    });
};
try { Sys.Application.notifyScriptLoaded(); } catch (e) { };