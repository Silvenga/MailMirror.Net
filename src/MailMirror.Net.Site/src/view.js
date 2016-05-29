import {HttpClient} from 'aurelia-fetch-client';

export class View {
    
    static inject() { return [HttpClient]; }
    
    constructor(http) {
        
        http.configure(config => {
            config
                .useStandardConfiguration()
                .withBaseUrl('https://api.github.com/');
        });
        
        this.http = http;
    }

    activate(params, routeConfig, navigationInstruction) {
        
        console.log(params.id);
        this.test = params.id;

        return this.http.fetch('users')
            .then(response => response.json())
            .then(users => this.users = users);
    }
}
