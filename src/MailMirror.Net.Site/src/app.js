export class App {
    configureRouter(config, router) {
        config.title = 'MxMirror';
        config.map([
            { route: ['', 'home'], name: 'home', moduleId: 'home', nav: true, title: 'Home' },
            { route: ['mail/:id/view'], name: 'list', moduleId: 'messages/list', nav: false }
        ]);

        this.router = router;
    }
}