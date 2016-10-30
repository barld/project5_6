class FormHandler{

	constructor(data){
		this.formData = new FormData();
		this.data = data;
		this.formData.append('data', this.data);
	}

	addData(identifier, data){
		this.formData.append(identifier, data);
	}
}