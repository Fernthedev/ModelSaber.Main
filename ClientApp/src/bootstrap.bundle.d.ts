declare module "bootstrap/dist/js/bootstrap.bundle.min" {
    import Alert from 'bootstrap/js/dist/alert';
    import Button from 'bootstrap/js/dist/button';
    import Carousel from 'bootstrap/js/dist/carousel';
    import Collapse from 'bootstrap/js/dist/collapse';
    import Dropdown from 'bootstrap/js/dist/dropdown';
    import Modal from 'bootstrap/js/dist/modal';
    import Offcanvas from 'bootstrap/js/dist/offcanvas';
    import Popover from 'bootstrap/js/dist/popover';
    import ScrollSpy from 'bootstrap/js/dist/scrollspy';
    import Tab from 'bootstrap/js/dist/tab';
    import Toast from 'bootstrap/js/dist/toast';
    import Tooltip from 'bootstrap/js/dist/tooltip';

    interface JQuery {
        alert: Alert.jQueryInterface;
        button: Button.jQueryInterface;
        carousel: Carousel.jQueryInterface;
        collapse: Collapse.jQueryInterface;
        dropdown: Dropdown.jQueryInterface;
        tab: Tab.jQueryInterface;
        modal: Modal.jQueryInterface;
        offcanvas: Offcanvas.jQueryInterface;
        [Popover.NAME]: Popover.jQueryInterface;
        scrollspy: ScrollSpy.jQueryInterface;
        toast: Toast.jQueryInterface;
        [Tooltip.NAME]: Tooltip.jQueryInterface;
    }

    interface Element {
        addEventListener(
            type: Carousel.Events,
            listener: (this: Element, ev: Carousel.Event) => any,
            options?: boolean | AddEventListenerOptions,
        ): void;
    }

    export { Alert, Button, Carousel, Collapse, Dropdown, Modal, Offcanvas, Popover, ScrollSpy, Tab, Toast, Tooltip };
}