

export class App {
    configureRouter(config, router) {
        config.title = 'MxMirror';
        config.map([
            { route: ['', 'home'], name: 'home', moduleId: 'home', nav: true, title: 'Home' },
            { route: ['mail/:id/view'], name: 'view', moduleId: 'view', nav: false }
        ]);

        this.router = router;
    }
}