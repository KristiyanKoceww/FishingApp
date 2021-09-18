import React,{Component} from 'react';
import { variables } from './Variables';

export class Reservoir extends Component{

    constructor(props){
        super(props);
        this.state={ reservoir:[]}
    }

    refreshList(){
        fetch('https://localhost:44366/api/Reservoir')
        .then(r => r.json())
    .then(data => {
        this.setState({reservoir:data})
    });
    }

    componentDidMount(){
        this.refreshList();
    }

    render(){
        const {reservoir}=this.state;
        const key = 1;
        return(
            <div>
                <table className="table table-striped">
                    <thead>
                        <tr className="text-justify">
                            <th className="text-justify">
                               Reservoirs
                            </th>
                        </tr>
                        <tr>
                            <th>Name</th>
                            <th>Type</th>
                            <th>Description</th>
                            <th>Latitude</th>
                            <th>Longitude</th>
                        </tr>
                    </thead>
                    <tbody>
                        {reservoir.map(d=>
                            <tr key={Math.random()}>
                                <td>{d.Name}</td>
                                <td>{d.Type}</td>
                                <td>{d.Description}</td>
                                <td>{d.Latitude}</td>
                                <td>{d.Longitude}</td>
                                </tr>
                                )}
                    </tbody>
                </table>
            </div>
        )
    }
}