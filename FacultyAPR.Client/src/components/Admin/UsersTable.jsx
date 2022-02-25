import React from 'react'
import { Table, Button } from 'reactstrap';
import UserModalForm from './UserModal'
import {USER_BASE_URL} from '../../js/constants.js'
import './management.css'
const USERS_URL = process.env.REACT_APP_SERVER_BASE_URL + "/" + USER_BASE_URL + "/";


function UserTable(props){
  const deleteUser = (id, userType) => {
    let confirmDelete = window.confirm('Are you sure you want to delete the user?')
    if(confirmDelete){
    fetch(USERS_URL + id + "/" + userType, {
      method: 'delete',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        id
      })
    })
      .then(response => response.json())
      .then(user => {
        props.deleteUserFromState(id)
      })
      .catch(err => console.log(err))
    }
  }

  const users = props.users.map(user => {
    return (
      <tr key={user.id}>
        <td>{user.firstName}</td>
        <td>{user.lastName}</td>
        <td>{user.emailAddress}</td>
        <td>{user.userType}</td>
        <td>
          <div className= "edit-buttons">
          <div>
            <UserModalForm className = "edit-user" buttonLabel="Edit" user={user} updateState={props.updateState}/>
            </div>

            <div>
            <Button color="danger" onClick={() => deleteUser(user.id, user.userType)}>Delete</Button>
            </div>
            </div>
        </td>
      </tr>
      )
    })

  return (
    <Table responsive hover variant dark>
      <thead>
        <tr>
          <th>First Name</th>
          <th>Last Name</th>
          <th>Email Address</th>
          <th>Position</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        {users}
      </tbody>
    </Table>
  )
}

export default UserTable