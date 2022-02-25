import React from 'react'
import Table from 'react-bootstrap/Table'
import {ActionDropdownButton} from './ActionDropdownButton'
const FormTable = () => {
    const buttonActions = [{'Name': "Remove", 'onClick': onClick}
            , {'Name': "Edit", 'onClick': onClick}]
    return (
        <Table striped bordered hover variant="dark">
            <thead>
                <tr>
                <th>Year</th>
                <th>Position</th>
                <th></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                <td>2019</td>
                <td>Associate Professor</td>
                <td><ActionDropdownButton title={"Actions"} actions={buttonActions}/></td>
                </tr>
                <tr>
                <td>2020</td>
                <td>Professor</td>
                <td><ActionDropdownButton title={"Actions"} actions={buttonActions}/></td>
                </tr>
            </tbody>
        </Table>
    )
}

const onClick = (name) => {
    console.log(name);
}

export {FormTable};
