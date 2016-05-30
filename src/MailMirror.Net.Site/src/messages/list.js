import {HttpClient} from 'aurelia-fetch-client';

export class View {

    static inject() { return [HttpClient]; }

    constructor(http) {
        this.http = http;
        this.messages = [];
        this.id = "Unknown";
        this.selected = {};
    }

    activate(params, routeConfig, navigationInstruction) {
        this.id = params.id;
        return this.http.fetch(`api/messages/from/${params.id}`)
            .then(response => response.json())
            .then(messages => this.messages = messages);
    }
}
