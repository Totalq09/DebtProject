var Direction;
(function (Direction) {
    Direction[Direction["Up"] = 1] = "Up";
    Direction[Direction["Down"] = 2] = "Down";
})(Direction || (Direction = {}));
(function ($) {
    $.prototype.HasScrollBar = function () {
        return this.get(0).scrollHeight > this.height();
    };
})(jQuery);
var Scroller = /** @class */ (function () {
    function Scroller() {
        this.currentContainerSequenceNumber = 0;
        this.containersQuantity = 0;
        this.navbarHeight = 0;
        this.navbarLinks = $();
        this.bodyElement = $();
        this.containers = $();
        this.scrollable = true;
    }
    Scroller.prototype.SetMainContainersQuantity = function () {
        this.containersQuantity = $('section.main-container').length;
    };
    Scroller.prototype.SetBodyHTMLElement = function () {
        this.bodyElement = $('html body');
    };
    Scroller.prototype.SetContainersHTMLElements = function () {
        this.containers = $('section.main-container');
    };
    Scroller.prototype.SetNavbarHeight = function () {
        this.navbarHeight = $("nav.navbar").height();
    };
    Scroller.prototype.SetNavbarLinksHTMLElements = function () {
        this.navbarLinks = $('nav.navbar .navbar-nav .nav-item:not(.nav-item-login)');
    };
    Scroller.prototype.BindEvents = function () {
        var context = this;
        this.BindMouseWheelEvent();
        $(window).on("scroll touchmove", function (event) {
            //event.preventDefault();
            event.stopPropagation();
            return false;
        });
        $(window).on("resize", function () {
            context.ScrollToContainerCurrentContainerImmediately({ data: { context: context } });
            return false;
        });
        this.BindNavbarLinks();
        this.BindArrowKeyEvent();
    };
    Scroller.prototype.BindNavbarLinks = function () {
        var context = this;
        $("nav.navbar .navbar-brand").on("click", { sequenceNumber: 0, context: context }, context.ScrollToContainer);
        this.navbarLinks.each(function (index) {
            $(this).on("click", { sequenceNumber: index, context: context }, context.ScrollToContainer);
        });
    };
    Scroller.prototype.BindArrowKeyEvent = function () {
        $(document).keydown(function (e) {
            if (e.keyCode !== 38 && e.keyCode !== 40) {
                return;
            }
            //e.preventDefault();
            e.stopPropagation();
            var val = e.keyCode === 38 ? -1 : 1;
            var ev = $.Event("mousewheel", { detail: val });
            ev.originalEvent = $.Event("mousewheel", { detail: val });
            $(window).trigger(ev);
        });
    };
    Scroller.prototype.BindMouseWheelEvent = function () {
        var context = this;
        $(window).bind('mousewheel DOMMouseScroll ', function (event) {
            var direction = event.originalEvent.wheelDelta > 0 || event.originalEvent.detail < 0
                ? Direction.Up : Direction.Down;
            var divToCheck = $(context.containers[context.currentContainerSequenceNumber]).get(0);
            if (divToCheck.scrollHeight + 2.0 * context.navbarHeight > $(window).height()) {
                if (divToCheck.offsetHeight + divToCheck.scrollTop < divToCheck.scrollHeight && direction === Direction.Down) {
                    return;
                }
                if (divToCheck.scrollTop !== 0 && direction === Direction.Up) {
                    return;
                }
            }
            //event.preventDefault();
            event.stopPropagation();
            if (!context.scrollable) {
                return;
            }
            context.scrollable = context.Scroll(direction);
            $(window).unbind("mousewheel DOMMouseScroll");
            setTimeout(function () {
                context.BindMouseWheelEvent();
                context.scrollable = true;
            }, 400);
        });
    };
    Scroller.prototype.IsScrolledIntoView = function (elem) {
        var $elem = $(elem);
        var $window = $(window);
        var docViewTop = $window.scrollTop();
        var docViewBottom = docViewTop + $window.height();
        var elemTop = $elem.offset().top;
        var elemBottom = elemTop + $elem.height();
        return ((elemBottom <= docViewBottom) && (elemTop >= docViewTop));
    };
    Scroller.prototype.SetInitialContainer = function () {
        var context = this;
        this.containers.toArray().forEach(function (value, index) {
            if (context.IsScrolledIntoView(value)) {
                context.currentContainerSequenceNumber = index;
                context.navbarLinks.toArray().forEach(function (value) {
                    $(value)[0].classList.remove("active");
                });
                context.navbarLinks[context.currentContainerSequenceNumber].classList.add("active");
            }
        });
    };
    Scroller.prototype.Initialize = function () {
        this.SetMainContainersQuantity();
        this.SetBodyHTMLElement();
        this.SetContainersHTMLElements();
        this.SetNavbarHeight();
        this.SetNavbarLinksHTMLElements();
        this.SetInitialContainer();
        this.BindEvents();
    };
    Scroller.prototype.Scroll = function (direction) {
        if (direction === Direction.Down) {
            if (this.currentContainerSequenceNumber === this.containersQuantity - 1) {
                return false;
            }
            this.currentContainerSequenceNumber++;
            this.ScrollWholeContainerDown();
        }
        else {
            if (this.currentContainerSequenceNumber === 0) {
                return false;
            }
            this.currentContainerSequenceNumber--;
            this.ScrollWholeContainerUp();
            return true;
        }
    };
    Scroller.prototype.ScrollWholeContainerDown = function () {
        var context = this;
        this.navbarLinks[context.currentContainerSequenceNumber - 1].classList.remove("active");
        this.bodyElement.animate({
            scrollTop: context.containers[context.currentContainerSequenceNumber].offsetTop - this.navbarHeight
        }, 500);
        $("body").css("background-color", $(this.containers[this.currentContainerSequenceNumber - 1]).css("background-color")),
            $("body").animate({ "background-color": $(context.containers[this.currentContainerSequenceNumber]).css("background-color") }, 200),
            this.navbarLinks[context.currentContainerSequenceNumber].classList.add("active");
    };
    Scroller.prototype.ScrollWholeContainerUp = function () {
        var context = this;
        this.navbarLinks[this.currentContainerSequenceNumber + 1].classList.remove("active");
        this.bodyElement.animate({
            scrollTop: context.containers[context.currentContainerSequenceNumber].offsetTop - this.navbarHeight
        }, 500);
        $("body").css("background-color", $(this.containers[this.currentContainerSequenceNumber + 1]).css("background-color")),
            $("body").animate({ "background-color": $(context.containers[this.currentContainerSequenceNumber]).css("background-color") }, 200),
            this.navbarLinks[context.currentContainerSequenceNumber].classList.add("active");
    };
    Scroller.prototype.ScrollToContainer = function (event) {
        var sequenceNumber = event.data.sequenceNumber;
        var context = event.data.context;
        if (sequenceNumber >= context.containers.length) {
            return;
        }
        var previousContainerSequenceNumber = context.currentContainerSequenceNumber;
        context.currentContainerSequenceNumber = sequenceNumber;
        context.navbarLinks.toArray().forEach(function (value) {
            $(value)[0].classList.remove("active");
        });
        context.bodyElement.animate({
            scrollTop: context.containers[context.currentContainerSequenceNumber].offsetTop - context.navbarHeight
        }, 500);
        $("body").css("background-color", $(context.containers[previousContainerSequenceNumber]).css("background-color")),
            $("body").animate({ "background-color": $(context.containers[context.currentContainerSequenceNumber]).css("background-color") }, 200),
            context.navbarLinks[context.currentContainerSequenceNumber].classList.add("active");
    };
    Scroller.prototype.ScrollToContainerCurrentContainerImmediately = function (event) {
        var context = event.data.context;
        context.bodyElement.animate({
            scrollTop: context.containers[context.currentContainerSequenceNumber].offsetTop - context.navbarHeight
        }, 0);
    };
    return Scroller;
}());
$(document).ready(function () {
    var scroller = new Scroller();
    scroller.Initialize();
    jQuery.fx.interval = 11;
});
//# sourceMappingURL=scroller.js.map