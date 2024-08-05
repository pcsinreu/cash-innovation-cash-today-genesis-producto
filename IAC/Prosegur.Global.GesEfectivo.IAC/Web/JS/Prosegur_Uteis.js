 var evento = window.Event ? true : false;

// -----------------------------------------------------------------------------
// <summary>
// Para permitir a digitação apenas de números inteiros positivos
// </summary>
// <param name=""evt"">Recupera o evento da pagina</param>
// <param name=""aceitarNegativo"">True para a função permitir números negativos</param>
// <history>
// 	[rafael.gans]	30/03/2006	Created
// </history>
// -----------------------------------------------------------------------------
function _validarInteiro(evt, aceitarNegativo, campo){
	var tecla = evento ? evt.which : evt.keyCode;
	var resultado = true;

	//Apenas números
	resultado = (tecla <= 13 || (tecla >= 48 && tecla <= 57) || (tecla >= 96 && tecla <= 105));

	if(aceitarNegativo == true){
		if(resultado == false && (tecla == 109 || tecla == 189) && campo.value.length == 0)
			resultado = true;
	}

	return resultado;
}


// -----------------------------------------------------------------------------
// <summary>
// Efetua o agrupamento da coluna de uma listagem pelo nome igual.
// </summary>
// <param name="objTabela">Objeto ou nome representando a tabela a ser agrupada</param>
// <param name="posCell">Índice da coluna a ser agrupada</param>
// <remarks>
//	Bug conhecido: Ao agrupar uma tabela gerada pelo componente DataGrid do vs.Net
//				   tenha certeza de que as colunas visíveis são as últimas da coleção.
//				   Se existir coluna invisível no final da coleção a tabela será grupada erroniamente.
// </remarks>
// <history>
// 	[rafael.gans]	19/09/2006	Created
// </history>
// -----------------------------------------------------------------------------
function _agruparColuna(objTabela, posCell){
	var oTabela = '';
	var textCell = '';
	var totalRows = '';
	var posFixa = 0;
	var posProxima = posFixa + 1;
	var rSpan = 2;

	//Verifica se a tabela existe senão retorna falso
	oTabela = typeof(objTabela) == 'object' ? objTabela : document.getElementById(objTabela);
	if(oTabela == null) return false;
	//Recupera o total de linha da tabela
	totalRows = oTabela.rows.length;
	//Se a posição da coluna informada não é válida faz o agrupamento da primeira coluna
	if(!isNaN(posCell)) posCell = 0;
	//Verifica se a tabela tem pelo menos 1 linha
	if(totalRows < 1) return false;
	//Verifica se a tabela contém a coluna na posição informada
	if(posCell > oTabela.rows(0).cells.length - 1) return false;

	//Para cada linha verifica se a proxima tem o mesmo valor
	//se sim deleta a proxima coluna e adiciona o rowspan
	while(posFixa < totalRows){
		//Se chegou ao final das linhas retorna true
		if(posProxima >= totalRows) return true;
		textCell = oTabela.rows(posProxima).cells(posCell).innerText;
		with(oTabela.rows(posFixa).cells(posCell)){
			//className = oTabela.rows(posFixa).className;
			if(innerText == textCell){
				setAttribute('rowSpan',rSpan);
				oTabela.rows(posProxima).deleteCell(posCell);
				//Se deletou a coluna igual incrementa até a próxima coluna
				posProxima++;
				rSpan++;
			}else{
				//className = className == 'GRadeItemImpar' ? 'GRadeItemPar' : 'GRadeItemImpar';
				//Se a próxima linha é logo abaixo da linha atual incrementa as duas
				//senão seta a linha na mesma posição da próxima linha
				if(posProxima == posFixa + 1){
					posFixa++;
				}else{
					posFixa = posProxima;
				}
				posProxima++;
				rSpan = 2;
			}
		}
	}
}

/*
		Funções cliente para formatação e validação de campos do tipo hora
		Copyright © 2005 - Prosegur Brasil
*/

function CompletaHora(obj)

{

	if (obj.value.length==1)

	obj.value+='0:00';

	if (obj.value.length==2)

	obj.value+=':00';

	if (obj.value.length==3)

	obj.value+='00';

	if (obj.value.length==4)

	obj.value+='0';

}

function ForcaHora()

{

	if (((event.keyCode>=48)&&(event.keyCode<=57)))

	{

	tecla = parseInt(event.keyCode) - 48;

	arr = event.srcElement.value.split(':');

	if (event.srcElement.value.length==2)
	{

	if (tecla<6)

	event.srcElement.value += ':' + tecla;

	return(false);

	}


	if (event.srcElement.value.length==5)

	return(false);

	if ((event.srcElement.value.length==0) && (tecla>2))

	{

	event.srcElement.value = '0' + tecla ;

	return(false);

	}

	if ((event.srcElement.value.length==3) && (tecla>5))

	{

	return(false);

	}


	if (event.srcElement.value.length==1)

	{

	if((event.srcElement.value>=2) && (tecla>3))

	return(false);

	}


	}

	else

	{

	return(false);

} 
}

function ForcaData()
{
	//if (event.srcElement.value.length <= 6) { - usar o maxlength do campo
		
	if ((event.keyCode == 47)||(event.keyCode == 44))
		return false;	
	// Limita caracteres válidos - somente números
	if (((event.keyCode>=48)&&(event.keyCode<=57)))
	{
		//Valida Ano
		//Insere a "/" entre mes e ano
		if (event.srcElement.value.length==2)
		{
			event.srcElement.value += '/' //+ tecla;
		}
		//1o digito do Ano deve ser maior ou igual a 1 e menor ou igual a 3 
		if (event.srcElement.value.length==3)
		{
			if ((!(event.keyCode >= 49))||(event.keyCode >= 51))
			{
				return false;
			}
		}
					
		//Valida mes
		//1o dígito deve ser 0 ou 1
		if (event.srcElement.value.length==0)
		{
			if ((event.keyCode != 48) && (event.keyCode != 49))
			{
				return false;
			}
		}
		else if (event.srcElement.value.length == 1)
		{
			if (event.srcElement.value == 1)
			{
				if ((event.keyCode != 48) && (event.keyCode != 49) && (event.keyCode != 50))
				{
					return false;
				}
			}
			else if (event.srcElement.value == 0)
			{
				if (event.keyCode == 48)
				{
					return false;
				}
			}
		}
	}
	else
		return false;
}

// <summary>
//  abre popup e seta foco para ele
// </summary>
// <param name=""evt"">Recupera o evento da pagina</param>
// <param name=""aceitarNegativo"">True para a função permitir números negativos</param>
// <history>
// 	 [bruno.costa]  17/09/2008  Created
// </history>
// -----------------------------------------------------------------------------
function AbrirPopupComFoco(url, nome, altura, largura, outrosParametros)
{
				var esq = (screen.availWidth/2) - (largura/2); 
				var topo = (screen.availHeight/2) - (altura/2);
				
				var outros = 'left=' + esq + ', top=' + topo + ', width=' + largura + ', height=' + altura + outrosParametros;

				newwindow = window.open(url, nome, outros);									
				
				// seta foco para o popup
				if (window.focus) {newwindow.focus()}
					return false;
					
}
			
// -----------------------------------------------------------------------------
// <summary>
// Abre um popup centralizado na tela
// </summary>
// <param name="url">Recupera o evento da pagina</param>
// <param name="nome">True para a função permitir números negativos</param>
// <param name="altura">True para a função permitir números negativos</param>
// <param name="largura">True para a função permitir números negativos</param>
// <param name="outrosParametros">True para a função permitir números negativos</param>
// <history>
// 	[bruno.costa]	01/02/2008	Created
// </history>
// -----------------------------------------------------------------------------
function AbrirPopup(url, nome, altura, largura, outrosParametros)
{
	var esq = (screen.availWidth/2) - (largura/2); 
	var topo = (screen.availHeight/2) - (altura/2);
	
	var outros = 'left=' + esq + ', top=' + topo + ', width=' + largura + ', height=' + altura + outrosParametros;
	
	return window.open(url, nome, outros);				
}

// -----------------------------------------------------------------------------
// <summary>
// Impede usuário de colar texto em campos numéridos. Deve ser registrado no evento OnKeyDown do textbox
// </summary>
// <history>
// 	[bruno.costa]	02/04/2008	Created
// </history>
// -----------------------------------------------------------------------------
function ImpedirColarTexto(){
    var ctrl=window.event.ctrlKey;
     var teclaShift = window.event.shiftKey;
     var tecla=window.event.keyCode;
     if ((ctrl && tecla==86) || (teclaShift && tecla==45)) {   // (CTRL+V) && (SHIFT+Ins)
         event.keyCode=0; 
         event.returnValue=false;}
}

// -----------------------------------------------------------------------------
// <summary>
// Função que não permite a digitação de caracteres inválidos (',<,\')
// </summary>
// <history>
// 	[bruno.costa]	02/04/2008	Created
// </history>
// -----------------------------------------------------------------------------
function ehCaracterValido(evt){ 
	var charCode = (evt.which) ? evt.which : event.keyCode; 
		if ( charCode == 39 || charCode == 60 || charCode == 92 ) 
			return false;
		return true;
		}

// -----------------------------------------------------------------------------
// <summary>
// Função javascript que permite somente a digitação de dígitos numéricos num textbox
// </summary>
// <history>
// 	[bruno.costa]	02/04/2008	Created
// </history>
// -----------------------------------------------------------------------------
function ehCaracterNumerico(evt){ 
	var charCode = (evt.which) ? evt.which : event.keyCode; 
		if (charCode > 31 && (charCode < 48 || charCode > 57)) 
			return false;
		return true;
		}

// -----------------------------------------------------------------------------
// <summary>
// Função javascript que permite a digitação de dígitos numéricos, ponto e sinal menor num textbox (CPF)
// </summary>
// <history>
// 	[bruno.costa]	02/04/2008	Created
// </history>
// -----------------------------------------------------------------------------
function txtCPF(evt){ 
	var charCode = (evt.which) ? evt.which : event.keyCode; 
		if (charCode > 31 && ((charCode < 48 && charCode != 46 && charCode != 45 ) || charCode > 57) ) 
			return false;
		return true;
		}

function currency(which){
		currencyValue = which.value;
		currencyValue = currencyValue.replace(",", "");
		decimalPos = currencyValue.lastIndexOf(".");
		if (decimalPos != -1){
				decimalPos = decimalPos + 1;
		}
		if (decimalPos != -1){
				decimal = currencyValue.substring(decimalPos, currencyValue.length);
				if (decimal.length > 2){
						decimal = decimal.substring(0, 2);
				}
				if (decimal.length < 2){
						while(decimal.length < 2){
							 decimal += "0";
						}
				}
		}
		if (decimalPos != -1){
				fullPart = currencyValue.substring(0, decimalPos - 1);
		} else {
				fullPart = currencyValue;
				decimal = "00";
		}
		newStr = "";
		for(i=0; i < fullPart.length; i++){
				newStr = fullPart.substring(fullPart.length-i-1, fullPart.length - i) + newStr;
				if (((i+1) % 3 == 0) & ((i+1) > 0)){
						if ((i+1) < fullPart.length){
							 newStr = "," + newStr;
						}
				}
		}
		which.value = newStr + "." + decimal;
}

///<summary>
///     Formata campo no formato currency
//      Função copiada de outro projeto.
/// </summary>
/// <history>
///     [bruno.costa] 11/07/2008 Created
/// </history>
function currencyFormat(fld, milSep, decSep, e, len) {

if (fld.value.length > len)
    return false;

    var sep = 0;
    var key = '';
    var i = j = 0;
    var len = len2 = 0;
    var strCheck = '-0123456789';
    var aux = aux2 = '';
    var whichCode = (window.Event) ? e.which : e.keyCode;
    if (whichCode == 13)
    return true;  // Enter
    key = String.fromCharCode(whichCode);  // Get key value from key code
    if (strCheck.indexOf(key) == -1) return false;  // Not a valid key
    len = fld.value.length;
    for(i = 0; i < len; i++)
    if ((fld.value.charAt(i) != '0') && (fld.value.charAt(i) != decSep)) break;
    aux = '';
    for(; i < len; i++)
    if (strCheck.indexOf(fld.value.charAt(i))!=-1) aux += fld.value.charAt(i);
    aux += key;
    len = aux.length;

    if (len == 0) fld.value = '';

    // Aqui tem que testar: se $Column->{decimals} for 3 coloca + '0' + '0' + senão, apenas + '0' +
    if (len == 1) fld.value = '0' + decSep + '0' + aux; // Alterei coloquei + '0' , decSep = ,(vírgula)

    // Aqui tem que testar: se $Column->{decimals} for 3 coloca + '0' + senão, não coloca
    if (len == 2) fld.value = '0' + decSep + aux; // Alterei coloquei + '0'

    // Aqui tem que testar: se $Column->{decimals} for 3 coloca essa linha senão não coloca
    //if (len == 3) fld.value = '0' + decSep + aux + aux2; // Adicionei esta linha

    // Aqui tem que colocar a variável $decimal
    if (len > 2) { // Alterei de 2 para 3
     aux2 = '';

     // A cada três caracteres adiciona um milSep (ponto ".")
     for (j = 0, i = len - 3; i >= 0; i--) {
      if (j == 3) {
       aux2 += milSep;
       j = 0;
      }
      aux2 += aux.charAt(i);
      j++;
     }
     

     fld.value = '';
     len2 = aux2.length;

     // Aqui tem que testar: se $Column->{decimals} for 3 coloca i > 0 senão coloca i >= 0
     for (i = len2 - 1; i >= 0; i--) // Alterei de i >= 0 para i > 0
     fld.value += aux2.charAt(i);

     // Aqui tem que coloca a variável mo lugar do 3
     fld.value += decSep + aux.substr(len - 2, len); // O número dois é o valor chave (número de casas que vem depois da vírgula)
     
     //Retira o ponto após ser digitado("-") 	
     if (fld.value.indexOf("-") == 0){
	    if (fld.value.indexOf(".") == 1){
		    fld.value = fld.value.replace(".","")
	    }
     }
       
    }
    return false;
}

///<summary>
///  Confirma evento Fechar de um popup
/// </summary>
/// <history>
///     [bruno.costa] 16/09/2008 Created
/// </history>
function ConfirmarFechar(msg)
{
				var resp;
				resp = window.confirm(msg);
				if (resp) {window.close();} 
}