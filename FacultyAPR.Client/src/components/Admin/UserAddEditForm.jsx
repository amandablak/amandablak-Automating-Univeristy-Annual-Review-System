import React, { useState, useEffect } from 'react';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import {USER_BASE_URL} from '../../js/constants.js'
const USERS_URL = process.env.REACT_APP_SERVER_BASE_URL + "/" + USER_BASE_URL + "/";

function UserAddEditForm(props) {
  const[form, setValues] = useState({
    id:'',
    firstName: '',
    lastName: '',
    emailAddress: '',
    userType: ''
  })

  const onChange = event => {
    setValues({
      ...form,
      [event.target.name]: event.target.value
    })
  }

  const submitFormAdd = event => {
    event.preventDefault()
    fetch(USERS_URL, {
      method: 'post',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        firstName: form.firstName,
        lastName: form.lastName,
        emailAddress: form.emailAddress,
        userType: form.userType  
    })
})
    .then(response => response.json())
    .then(user => {
      if(Array.isArray(user)) {
        props.addUserToState(user[0])
        props.toggle()
      } else {
        console.log('failure')
      }
    })
    .catch(err => console.log(err))
  }

  const submitFormEdit = event => {
    event.preventDefault()
    fetch(USERS_URL, {
      method: 'patch',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        id: form.id,
        firstName: form.firstName,
        lastName: form.lastName,
        emailAddress: form.emailAddress,
        userType: form.userType
    })
})
.then(response => response.json())
.then(user => {
  if(Array.isArray(user)) {
    props.addUserToState(user[0])
    props.toggle()
  } else {
    console.log('failure')
  }
})
.catch(err => console.log(err))
}

  useEffect(() => {
    if(props.user){
      const { firstName, lastName, emailAddress, userType} = props.user
      setValues({ firstName, lastName, emailAddress, userType })
    }
  }, false)


  return (
    <Form onSubmit={props.user ? submitFormEdit : submitFormAdd}>
      <FormGroup>
        <Label for="firstName">First Name</Label>
        <Input type="text" name="firstName" id="firstName" onChange={onChange} value={form.firstName === null ? '' : form.firstName} />
      </FormGroup>
      <FormGroup>
        <Label for="lastName">Last Name</Label>
        <Input type="text" name="lastName" id="lastName" onChange={onChange} value={form.lastName === null ? '' : form.lastName}  />
      </FormGroup>
      <FormGroup>
        <Label for="emailAddress">Email Address</Label>
        <Input type="email" name="emailAddress" id="emailAddress" onChange={onChange} value={form.emailAddress === null ? '' : form.emailAddress}  />
      </FormGroup>
      <FormGroup>
        <Label for="userType">Position</Label>
        <Input type="text" name="userType" id="userType" onChange={onChange} value={form.userType === null ? '' : form.userType} />
      </FormGroup>
      <Button>Submit</Button>
    </Form>
  )
}

export default UserAddEditForm