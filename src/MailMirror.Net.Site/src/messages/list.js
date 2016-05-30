import {HttpClient} from 'aurelia-fetch-client';

export class View {

    static inject() { return [HttpClient]; }

    constructor(http) {
        this.http = http;
        this.id = "Unknown";
        this.selected = null;
        this.messages = [];
        this.isAttached = false;
    }

    update() {
        this.http.fetch(`api/messages/from/${this.id}`)
            .then(response => response.json())
            .then(messages => this.mergeMessages(messages));
        if (this.isAttached) {
            setTimeout(() => this.update.call(this), 1000 * 3);
        }
    }

    mergeMessages(messageList) {

        var localIds = this.messages.map(x => x.Id);
        var remoteIds = messageList.map(x => x.Id);

        // Add
        for (var i = 0; i < remoteIds.length; i++) {
            var id = remoteIds[i];
            var exists = localIds.indexOf(id);
            if (exists === -1) {
                var message = messageList[i];
                console.log(`New messages ${message.Id} found.`);
                this.messages.push(message);
            }
        }

        // Remove
        for (var i = 0; i < localIds.length; i++) {
            var id = localIds[i];
            var exists = remoteIds.indexOf(id);
            if (exists === -1) {
                var message = this.messages[i];
                console.log(`Removed messages ${message.Id} found.`);
                this.messages.splice(i, 1);
            }
        }
    }

    activate(params, routeConfig, navigationInstruction) {
        this.id = params.id;
    }

    attached() {
        this.isAttached = true;
        this.update();
    }

    detached() {
        this.isAttached = false;
    }
}
