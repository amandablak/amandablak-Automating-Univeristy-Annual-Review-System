import React from 'react'
import Table from 'react-bootstrap/Table'
import {useHistory} from 'react-router-dom'

const ReviewerTable = ({formState, formStubs}) => {
    var history = useHistory();
    const TableHeader = () => {
        let tableHeaderRow = ['Form Year', 'Faculty Name', 'State', 'Position'];

        return tableHeaderRow.map((key, index) => {
            return (<th key={index}>{key}</th>
            )
        })
    }
    const TableBody = (formState, history) => {
        try
        {
            return formStubs
            .filter(formStub => formStub.state === formState)
            .map(({facultyId, formYear, facultyName, state, rank, formId}) => {
                return (  
                    <tr key={facultyId}>
                        <td>{formYear}</td>
                        <td>{facultyName}</td>
                        <td>{state}</td>
                        <td>{rank}</td>      
                    </tr>    
                )
            })
        }
        catch(_) {}
        return null;
    }
    
    return (
        <Table striped bordered hover variant="dark">
            <thead>
                <tr>{TableHeader()}</tr>
            </thead>
            <tbody>
                {TableBody(formState, history)}
            </tbody>
        </Table>   

    )
}


export {ReviewerTable}