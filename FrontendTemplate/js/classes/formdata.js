class FormHandler{

	constructor(){
		this.formData = new FormData();
	}

	addValue(key, value){
		this.formData.append(key, value);
	}

	getValue(key){
		return this.formData.get(key);
	}

	removeValue(key){
		this.formData.delete(key);
	}

	// Start asynchronous interaction with the server
	startCall(options){


		var xhr = new XMLHttpRequest();

		// The RequestHeader can be any, by the server accepted, file
		xhr.setRequestHeader(options.requestHeader);

		// POST is the most safe method to send data to the server
		xhr.open('POST', options.url);

		// Function to fire off when the server has send a response
		xhr.onload = function(){
			options.ajaxFunction();
		}
	}
}