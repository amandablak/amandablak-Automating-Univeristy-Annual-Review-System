import React, { useState, useEffect } from 'react'
import { Container, Row, Col } from 'reactstrap'
import UserModalForm from './UserModal'
import UserTable from './UsersTable'
import {USER_BASE_URL} from '../../js/constants.js'
const USERS_URL = process.env.REACT_APP_SERVER_BASE_URL + "/" + USER_BASE_URL + "/";


function Users(props) {

  const [users, setUsers] = useState([])

  const getUsers= () => {
    fetch(USERS_URL + "All")
      .then(response => response.json())
      .then(users => setUsers(users))
      .catch(err => console.log(err))
  }

  const addUserToState = (user) => {
    setUsers([...users, user])
  }

  const updateState = (user) => {
    const userIndex = users.findIndex(data => data.id === user.id)
    const newArray = [...users.slice(0, userIndex), user, ...users.slice(userIndex + 1)]
    setUsers(newArray)
  }

  const deleteUserFromState = (id) => {
    const updatedUsers = users.filter(user => user.id !== id)
    setUsers(updatedUsers)
  }

  useEffect(() => {
    getUsers()
  }, []);

  return (
      <Container className="Users">
        <Row>
          <Col>
            <UserTable users={users} updateState={updateState} deleteUserFromState={deleteUserFromState} />
          </Col>
        </Row>
        <Row>
          <Col>
            <UserModalForm className = "new-user" buttonLabel="Add New User" addUserToState={addUserToState}/>
          </Col>
        </Row>
      </Container>
  )
}

export default Users