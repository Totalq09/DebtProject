enum Direction{
    Up = 1,
    Down
}

(function($) {
    $.prototype.HasScrollBar = function() {
        return this.get(0).scrollHeight > this.height();
    }
})(jQuery);

class Scroller{
    private currentContainerSequenceNumber: number;
    private containersQuantity: number;
    private navbarHeight: number;

    private bodyElement: JQuery<HTMLElement>;
    private containers: JQuery<HTMLElement>;
    private navbarLinks: JQuery<HTMLElement>;

    private scrollable: boolean;

    constructor(){
        this.currentContainerSequenceNumber = 0;
        this.containersQuantity = 0;
        this.navbarHeight = 0;
        this.navbarLinks = $();
        this.bodyElement = $();
        this.containers = $();
        this.scrollable = true;
    }

    private SetMainContainersQuantity(): void{
        this.containersQuantity = $('section.main-container').length;
    }

    private SetBodyHTMLElement(): void{
        this.bodyElement = $('html body');
    }

    private SetContainersHTMLElements(): void{
        this.containers = $('section.main-container');
    }

    private SetNavbarHeight(): void{
        this.navbarHeight = $("nav.navbar").height();
    }

    private SetNavbarLinksHTMLElements(): void{
        this.navbarLinks = $('nav.navbar .navbar-nav .nav-item:not(.nav-item-login)');
    }

    private BindEvents(): void{
        let context = this;
        this.BindMouseWheelEvent();

        $(window).on("scroll touchmove",function(event) {
          //event.preventDefault();
           event.stopPropagation();
           return false;
        });

        $(window).on("resize",function() {
            context.ScrollToContainerCurrentContainerImmediately({data: {context: context}});
            return false;
         });

        this.BindNavbarLinks();
        this.BindArrowKeyEvent();
    }

    private BindNavbarLinks(): void{
        var context = this;
        $("nav.navbar .navbar-brand").on("click", {sequenceNumber: 0, context: context}, context.ScrollToContainer);
        this.navbarLinks.each(function(index){
            $(this).on("click", {sequenceNumber: index, context: context}, context.ScrollToContainer);
        });
    }

    private BindArrowKeyEvent(): void{
        $(document).keydown(function(e){
            if (e.keyCode !== 38 && e.keyCode !== 40) {
                return;
            }
            //e.preventDefault();
            e.stopPropagation();

            let val: number = e.keyCode === 38 ? -1 : 1;
            let ev = $.Event("mousewheel", {detail: val});
            (<any>ev).originalEvent = $.Event("mousewheel", {detail: val});
            $(window).trigger(ev);
        });
    }

    private BindMouseWheelEvent(): void{
        var context = this;

        $(window).bind('mousewheel DOMMouseScroll ', function (event) {   
            let direction: Direction = 
            (<any>event).originalEvent.wheelDelta > 0 || (<any>event).originalEvent.detail < 0 
                ? Direction.Up : Direction.Down;

            let divToCheck = $(context.containers[context.currentContainerSequenceNumber]).get(0);
            if(divToCheck.scrollHeight + 2.0*context.navbarHeight > $(window).height()){
                if(divToCheck.offsetHeight + divToCheck.scrollTop < divToCheck.scrollHeight && direction === Direction.Down){
                    return;
                }
                if(divToCheck.scrollTop !== 0 && direction === Direction.Up){
                    return;
                }
            }

            //event.preventDefault();
            event.stopPropagation();

            if(!context.scrollable){
                return;
            }

            context.scrollable = context.Scroll(direction);

            $(window).unbind("mousewheel DOMMouseScroll");
            setTimeout(() => {
                context.BindMouseWheelEvent();
                context.scrollable = true;
            },400);
       });
    }

    private IsScrolledIntoView(elem): boolean{
        var $elem = $(elem);
        var $window = $(window);
    
        var docViewTop = $window.scrollTop();
        var docViewBottom = docViewTop + $window.height();
    
        var elemTop = $elem.offset().top;
        var elemBottom = elemTop + $elem.height();
    
        return ((elemBottom <= docViewBottom) && (elemTop >= docViewTop));
    }

    public SetInitialContainer(): void{
        let context = this;
        this.containers.toArray().forEach(function(value, index){
            if(context.IsScrolledIntoView(value)){
                context.currentContainerSequenceNumber = index;
                context.navbarLinks.toArray().forEach(function(value){
                    $(value)[0].classList.remove("active");
                });
                context.navbarLinks[context.currentContainerSequenceNumber].classList.add("active");
            }
        })
    }

    public Initialize(): void{
        this.SetMainContainersQuantity();
        this.SetBodyHTMLElement();
        this.SetContainersHTMLElements();
        this.SetNavbarHeight();
        this.SetNavbarLinksHTMLElements();
        this.SetInitialContainer();
        this.BindEvents();
    }

    public Scroll(direction: Direction) : boolean{
        if(direction === Direction.Down){
            if(this.currentContainerSequenceNumber === this.containersQuantity - 1){
                return false;
            }
            
            this.currentContainerSequenceNumber++;
            this.ScrollWholeContainerDown();
        }
        else{
            if(this.currentContainerSequenceNumber === 0){
                return false;
            }
            
            this.currentContainerSequenceNumber--;
            this.ScrollWholeContainerUp();

            return true;
        }
    }
    
    public ScrollWholeContainerDown() : void{
        var context = this;
        this.navbarLinks[context.currentContainerSequenceNumber-1].classList.remove("active");
        this.bodyElement.animate({
            scrollTop: context.containers[context.currentContainerSequenceNumber].offsetTop - this.navbarHeight
        }, 500);

        $("body").css("background-color", $(this.containers[this.currentContainerSequenceNumber-1]).css("background-color")), 
        $("body").animate({"background-color": $(context.containers[this.currentContainerSequenceNumber]).css("background-color")}, 200), 

        this.navbarLinks[context.currentContainerSequenceNumber].classList.add("active");
    }

    public ScrollWholeContainerUp() : void{
        var context = this;
        this.navbarLinks[this.currentContainerSequenceNumber+1].classList.remove("active");
        this.bodyElement.animate({
            scrollTop: context.containers[context.currentContainerSequenceNumber].offsetTop - this.navbarHeight
        }, 500);

        $("body").css("background-color", $(this.containers[this.currentContainerSequenceNumber+1]).css("background-color")), 
        $("body").animate({"background-color": $(context.containers[this.currentContainerSequenceNumber]).css("background-color")}, 200), 

        this.navbarLinks[context.currentContainerSequenceNumber].classList.add("active");
    }

    public ScrollToContainer(event: any): any{
        let sequenceNumber = event.data.sequenceNumber;
        let context = event.data.context;
        if(sequenceNumber >= context.containers.length){
            return;
        }

        var previousContainerSequenceNumber = context.currentContainerSequenceNumber;
        context.currentContainerSequenceNumber = sequenceNumber;

        context.navbarLinks.toArray().forEach(function(value){
            $(value)[0].classList.remove("active");
        });

        context.bodyElement.animate({
            scrollTop: context.containers[context.currentContainerSequenceNumber].offsetTop - context.navbarHeight
        }, 500);

        $("body").css("background-color", $(context.containers[previousContainerSequenceNumber]).css("background-color")), 
        $("body").animate({"background-color": $(context.containers[context.currentContainerSequenceNumber]).css("background-color")}, 200), 

        context.navbarLinks[context.currentContainerSequenceNumber].classList.add("active");
    }

    public ScrollToContainerCurrentContainerImmediately(event: any): any{
        let context = event.data.context;

        context.bodyElement.animate({
            scrollTop: context.containers[context.currentContainerSequenceNumber].offsetTop - context.navbarHeight
        }, 0);
    }
}

$(document).ready(() => {
    let scroller: Scroller = new Scroller();
    scroller.Initialize();
    jQuery.fx.interval = 11;
});
