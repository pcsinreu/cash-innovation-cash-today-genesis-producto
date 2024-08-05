var PageRequestManagerEx =
{
_initialized : false,

init : function()
{

if (!PageRequestManagerEx._initialized)
{

    var _prm = Sys.WebForms.PageRequestManager.getInstance();
    var _callQueue = new Array();
    var _callQueueDivs = new Array();
    var _executingElement = null;
    var _ControlPrioridade;
    

    _prm = Sys.WebForms.PageRequestManager.getInstance();

    _prm.add_initializeRequest(initializeRequest);
    _prm.add_endRequest(endRequest);    

    PageRequestManagerEx._initialized = true;
}



function initializeRequest(sender, args)
{

 
        var adiv = $get('AlertDiv');  
        
        if (adiv != null)
        {                                       
           adiv.style.height = document.body.clientHeight;                                                                                                                                                           
        }
                
                         

        //Get the element which cause the postback
        var postBackElement = args.get_postBackElement();
            
        //Se existir algum item personalizado, exibe o panel correspondentes
        //caso contrário, exibe o geral    
        if (!((postBackElement.IdDivExibicao == 'undefined') || (postBackElement.IdDivExibicao == null)))
        {                                        
                Array.enqueue(_callQueueDivs, new Array(postBackElement.IdDivExibicao));                                                  
        }

        if (_prm.get_isInAsyncPostBack())
        {
                          
             //Se o elemento que executou a ação possui o atributo "PrioridadeExecucao" setado como "True"
             //então aborta as requisições que estão sendo processadas e deixa apenas a atual
             if (postBackElement.PrioridadeExecucao == 'True' || postBackElement.id == ControlPrioridade )
             {
             
                //Aborda as requisições enfileiradas
                _prm.abortPostBack();
                                                
                //Limpa a Fila
                _callQueue=new Array;
                //Retorna as Divs de exibição de progresso para a visibilidade de escondidas 
                 var _executingDiv;
                                   for (x in _callQueueDivs)
                                    {
                                          _executingDiv = _callQueueDivs[x].toString();                                  
                                          var adivCustom = $get(_executingDiv);                                                                  
                                          if (adivCustom != null)
                                          {
                                            adivCustom.style.visibility = 'hidden';
                                          }                                  
                                    }   
                                    _callQueueDivs = new Array();              
                 
                 //Mostra a Div Macro
                MostraDiv('visible','')
                                      
             }
             else
             {
             
                 //if we are here that means there already a call pending.                                                                       
                 //We need to check this otherwise it will abort the request which we made from the
                 //end request                                      
                if (_executingElement != postBackElement)
                {
                    // Grab the event argument value
                    var evArg = $get("__EVENTARGUMENT").value;                                                                               
                    
                    //Does not match which means it is another control
                    //which request the update, so cancel it temporary and 
                    //add it in the call queue
                                                   
                    args.set_cancel(true);
                    Array.enqueue(_callQueue, new Array(postBackElement, evArg, postBackElement.IdDivExibicao));
                                                              
                    //Mostra a Div
                    MostraDiv('visible','')
                                                                                                                     
                }
             }
            

            //Reset it as we are done with our matching
            _executingElement = null;
        }
        else
        {
             //Mostra a Div
             MostraDiv('visible','') 
               
        }   
                   
   
} 

function MostraDiv(Visibilidade, Mensagem)
{                    


                       
                       if (_callQueueDivs.length > 0)
                       {                                                     
                                   
                                   //Varre a lista de Divs personalizadas
                                   var _executingDiv;
                                   for (x in _callQueueDivs)
                                    {
                                          _executingDiv = _callQueueDivs[x].toString();                                  
                                          var adivCustom = $get(_executingDiv);                                                                  
                                          if (adivCustom != null)
                                          {
                                            adivCustom.style.visibility = Visibilidade;
                                          }                                  
                                    } 
                                    
                                    if (Visibilidade=='hidden')
                                    {
                                        //Limpa o array
                                        _callQueueDivs = new Array();
                                    }
                       }
                       else
                       {
                                    //Div Geral
                                    var adiv = $get('AlertDiv');  
                                    
                                    if (adiv != null)
                                    {
                                       adiv.style.visibility = Visibilidade;
                                       adiv.style.height = document.body.clientHeight;
                                       
                                       
                                       
                                       if (Mensagem != '')                       
                                       adiv.innerHTML = Mensagem                                      
                                    }
                                                                                                       
                       }                                                                                                                                 

}

function endRequest(sender, args)
    {        

            var ContinuarExibindoDiv=false;                         
                                                                              
              //Check if we have a pending call
            if (_callQueue.length > 0)
            {
                //Get the first item from the call queue and setting it
                //as current executing item
                _executingElement = Array.dequeue(_callQueue);
                                
                //Continua exibindo a Div              
                ContinuarExibindoDiv=true
               
                var _element = _executingElement[0];
                var _eventArg = _executingElement[1];                                      
                                                                                        
             
                //Now Post the from which will also fire the initializeRequest
                //passar id em páginas normais
                _prm._doPostBack(_element.name, _eventArg);
                     
                                 
            }
            
            if (ContinuarExibindoDiv)
            {
                        //Mostra a Div
                        MostraDiv('visible','')
                                                          
            }
            else
            {
                        //Mostra a Div
                        MostraDiv('hidden','')
            
            }                                                                                                                                       
            
   } 

}

 

}




if (typeof(Sys) != 'undefined')
{
Sys.Application.notifyScriptLoaded();
}

