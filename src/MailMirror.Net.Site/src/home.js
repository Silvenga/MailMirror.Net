import {HttpClient} from 'aurelia-fetch-client';
import {Router} from "aurelia-router";

export class Home {
    static inject() { return [HttpClient, Router]; }

    constructor(client, router) {
        this.client = client;
        this.router = router;
        this.email = "m@silvenga.com";
    }
    submit() {
        this.router.navigateToRoute('list', { id: this.email })
    }
}