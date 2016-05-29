import {HttpClient} from 'aurelia-fetch-client';

export class Home {
    static inject() { return [HttpClient]; }

    constructor(client) {
        this.client = client;
        this.loopId = "";
    }
    submit() {
        router.navigateToRoute('routeName', { id: 123 })
    }

    activate(params, routeConfig) {
        
    }
}