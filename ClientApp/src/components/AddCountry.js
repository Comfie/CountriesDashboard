import {Component} from "react";
import {Redirect} from "react-router";

export class AddCountry extends Component {
    static displayName = AddCountry.name;

    constructor(props) {
        super(props);
        this.onChangeName = this.onChangeName.bind(this);
        this.onChangeCode = this.onChangeCode.bind(this);
        this.saveCountry = this.saveCountry.bind(this);
        this.newCountry = this.newCountry.bind(this);
        this.state = {
            country: {
                id: null,
                name: "",
                countryCode: "",
                countryAdded: false,
                nameValid: false,
                codeValid: false,
            }
        };
    }

    onChangeName(e) {
        this.setState({
            name: e.target.value,
            nameValid: true
        });
    }

    onChangeCode(e) {
        this.setState({
            countryCode: e.target.value,
            codeValid: true,
        });
    }

    newCountry() {
        this.setState({
            id: null,
            name: "",
            countryCode: "",
            countryAdded: false,
            nameValid: false,
            codeValid: false,
        });
    }

    async saveCountry() {
        const data = {
            name: this.state.name,
            countryCode: this.state.countryCode
        };
        const requestOptions = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(data)
        };
        await fetch('/api/countries/addCountry', requestOptions)
            .then(response => {
                this.setState({
                    countryAdded: true
                });
                const data = response.json();
            })
            .catch(e => {
                console.log(e);
            });
    }


    render() {
        if (this.state.countryAdded) {
            return <Redirect to='/'/>
        }
        return (
            <div className="submit-form">
                <div>
                    <div className="form-group">
                        <label htmlFor="title">Country Name</label>
                        <input
                            type="text"
                            className="form-control"
                            required
                            value={this.state.name}
                            onChange={this.onChangeName}
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="description">Country Code (Iso2Code)</label>
                        <input
                            type="text"
                            className="form-control"
                            required
                            value={this.state.countryCode}
                            onChange={this.onChangeCode}
                        />
                    </div>
                    <button onClick={this.saveCountry} disabled={!this.state.codeValid || !this.state.codeValid}
                            className="btn btn-success">
                        Add Country
                    </button>
                </div>
            </div>
        );
    }

}
