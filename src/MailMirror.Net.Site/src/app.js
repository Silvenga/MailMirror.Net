import {BindingSignaler} from 'aurelia-templating-resources';

export class App {
    static inject() { return [BindingSignaler]; }
    constructor(signaler) {
        this.signaler = signaler;

        setInterval(() => signaler.signal('every-second'), 1000);
    }
    configureRouter(config, router) {
        config.title = 'MxMirror';
        config.map([
            { route: ['', 'home'], name: 'home', moduleId: 'home', nav: true, title: 'Home' },
            { route: ['mail/:id/view'], name: 'list', moduleId: 'messages/list', nav: false }
        ]);

        this.router = router;
    }
}