import React, {Component, useState} from 'react';
import Card from 'react-bootstrap/Card';
import ListGroup from 'react-bootstrap/ListGroup';
import CardGroup from 'react-bootstrap/CardGroup';
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';
import Button from 'react-bootstrap/Button';
import {Link} from 'react-router-dom';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = {countries: [], loading: true};
    }


    componentDidMount() {
        this.getCountries();
    }

    static async deleteCountry(id) {
        const response = await fetch('/api/countries/deleteCountry/' + id, {method: 'DELETE'});
        const data = await response.json();
        if (data === true) {
            window.location.reload(true);
        }
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderCountriesList(this.state.countries);

        return (
            <div>
                <Row>
                    <Col>
                        <div className="mb-2" style={{float: 'right'}}>
                            <Link to="/country">
                                <Button variant="primary">Add Country</Button>
                            </Link>

                        </div>
                    </Col>
                </Row>
                <div>
                    {contents}
                </div>
            </div>
        );
    }

    static renderCountriesList(countries) {
        return (
            <CardGroup>
                <Row xs={1} md={3} className="g-4">
                    {countries.map(country =>
                        <Col key={country.id}>
                            <Card
                                style={{width: '18rem'}}
                                className="mb-2 cards">
                                <Card.Body>
                                    <Row>
                                        <Col xs={8}><Card.Title>{country.name}</Card.Title></Col>
                                        <Col>
                                            <Button variant="danger" onClick={() => this.deleteCountry(country.id)}
                                                    style={{float: 'right'}}>X</Button>
                                        </Col>
                                    </Row>
                                    <ListGroup className="list-group-flush">
                                        <ListGroup.Item className="listStyle"><strong>Country
                                            Code:</strong> {country.iso2Code}</ListGroup.Item>
                                        <ListGroup.Item className="listStyle"><strong>Capital
                                            City:</strong> {country.capitalCity}</ListGroup.Item>
                                        <ListGroup.Item className="listStyle"><strong>Admin
                                            Region:</strong> {country.adminRegion}</ListGroup.Item>
                                        <ListGroup.Item className="listStyle"><strong>Region:</strong> {country.region}
                                        </ListGroup.Item>
                                        <ListGroup.Item className="listStyle"><strong>Lending
                                            Type:</strong> {country.lendingType}</ListGroup.Item>
                                        <ListGroup.Item
                                            className="listStyle"><strong>Latitude:</strong> {country.latitude}
                                        </ListGroup.Item>
                                        <ListGroup.Item
                                            className="listStyle"><strong>Longitude:</strong> {country.longitude}
                                        </ListGroup.Item>
                                    </ListGroup>
                                </Card.Body>
                            </Card>
                        </Col>
                    )}
                </Row>
            </CardGroup>
        )
    }

    async getCountries() {
        const response = await fetch('/api/countries/getCountries');
        const data = await response.json();
        this.setState({countries: data, loading: false});
    }
}
