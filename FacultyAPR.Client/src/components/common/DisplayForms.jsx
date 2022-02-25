import React from 'react'
import Table from 'react-bootstrap/Table'
import {ActionDropdownButton} from './ActionDropdownButton'
import {LoadForm} from '../../common/helpers/form'
import {useHistory} from 'react-router-dom'

const DisplayForms = ({formState, createForm, formStubs}) => {
    var history = useHistory();
    const TableHeader = () => {
        let tableHeaderRow = ['Year', 'State', 'Position'];

        return tableHeaderRow.map((key, index) => {
            return (<th key={index}>{key}</th>
            )
        })
    }
    const TableBody = (formState, history, user) => {
        try
        {
            return formStubs
            .filter(formStub => formStub.state === formState)
            .map(({facultyId, formYear, state, rank, formId, user}) => {
                return (  
                    <tr key={facultyId}>
                        <td>{formYear}</td>
                        <td>{state}</td>
                        <td>{rank}</td>
                        <td>
                        {
                        <ActionDropdownButton title={"Actions"} actions={[{'Name': "Edit", 'onClick': () => 
                            {
                                editForm(facultyId, formId, createForm).then((_) => history.push("/form"))}}]
                            }/>
                    
                            }
                            </td>   
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


const editForm = async (facultyId, formId, createForm) => {
   var form = await LoadForm(facultyId, "1e06676a-08f2-4151-85ca-ce5f490ce77d", formId)
   createForm(form);
}

export {DisplayForms}