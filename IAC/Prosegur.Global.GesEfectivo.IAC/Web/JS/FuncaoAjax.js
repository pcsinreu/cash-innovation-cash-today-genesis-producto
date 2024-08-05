Sys.Application.add_load(ApplicationLoadHandler)
function ApplicationLoadHandler(sender, args)
{
    if (!Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack())
    {
      Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(InitializeRequest);
      Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);           
      
    }
}

var divElem = 'AlertDiv';
var divElemAll = 'AlertDivAll';
var messageElem = 'AlertMessage';
//var exclusivePostBackElement = 'Button1';
//var exclusivePostBackElement = 'ctl00_ContentPlaceHolder1_btnSalvar';
var exclusivePostBackElement;

var lastPostBackElement;
var _callQueue = new Array();
function InitializeRequest(sender, args)
{ 
    var prm = Sys.WebForms.PageRequestManager.getInstance();
        
    
    //Configura a Altura do DivAll           
    ConfiguraActivateAlertDivAll()
    
    //Exibe o DivAll caso seja o controle de precedencia        
    if(args.get_postBackElement().id === exclusivePostBackElement)
    {        
        ActivateAlertDivAll('visible');
    }       
    
    if (prm.get_isInAsyncPostBack() && 
        args.get_postBackElement().id === exclusivePostBackElement) 
    {
        
        //Caso seja a mesma solicitação da precedencia, então cancela a requisição atual
        if (lastPostBackElement === exclusivePostBackElement)
        {
          args.set_cancel(true);
          //ActivateAlertDiv('visible', 'Já existe um postback executando. O Novo postback foi cancelado.');
          //setTimeout("ActivateAlertDiv('hidden','')", 1500);
        }
        else if (lastPostBackElement !== exclusivePostBackElement)
        {
             
            //Limpa a fila de requisições assincronas
           _callQueue = new Array()           
           //Aborda a requisição corrente
            prm.abortPostBack();           
                               
        }
    }
    else if (prm.get_isInAsyncPostBack() && 
             args.get_postBackElement().id !== exclusivePostBackElement)
    {
        if (lastPostBackElement === exclusivePostBackElement)
        {
            var postBackElement = args.get_postBackElement();
            var evArg = $get("__EVENTARGUMENT").value;      
             
            args.set_cancel(true);            
            //ActivateAlertDiv('visible', 'Já existe um postback executando. O Novo postback foi cancelado.');
            //setTimeout("ActivateAlertDiv('hidden','')", 1500);
            return
        } 
        else
        {
            var postBackElement = args.get_postBackElement();
            var evArg = $get("__EVENTARGUMENT").value; 
            args.set_cancel(true);
            Array.enqueue(_callQueue, new Array(postBackElement, evArg, postBackElement.IdDivExibicao));
                    
        }      
    }
    
    lastPostBackElement = args.get_postBackElement().id;      
}

function ActivateAlertDiv(visString, msg)
{
     var adiv = $get(divElem);
     var aspan = $get(messageElem);
     adiv.style.visibility = visString;
     aspan.innerHTML = msg;
}

function ActivateAlertDivAll(visString)
{
     var adiv = $get(divElemAll);     
     if (adiv != null)
     {                
         adiv.style.visibility = visString;
         adiv.style.height = document.body.clientHeight;        
     }
}

function ConfiguraActivateAlertDivAll()
{
     var adiv = $get(divElemAll);     
     if (adiv != null)
     {                   
           adiv.style.height = document.body.clientHeight;                                                                                                                                                           
     }

}

function endRequest(sender, args)
    { 
    
    
     if (_callQueue.length > 0)
            {
            
                var _executingElement;
                //Get the first item from the call queue and setting it
                //as current executing item
                _executingElement = Array.dequeue(_callQueue);
                                
                //Continua exibindo a Div              
                ContinuarExibindoDiv=true
               
                var _element = _executingElement[0];
                var _eventArg = _executingElement[1];                                      
                                                                                                                    
                lastPostBackElement = _element.id;  
                 
                
                //Now Post the from which will also fire the initializeRequest
                //passar id em páginas normais
                Sys.WebForms.PageRequestManager.getInstance()._doPostBack(_element.name, _eventArg);
                                                                                     
            }
      else
            {
            
                if (lastPostBackElement === exclusivePostBackElement)
                {
                    ActivateAlertDivAll('hidden');
                }
            
           }
        
    }       


if (typeof(Sys) != 'undefined')
{
Sys.Application.notifyScriptLoaded();
}
