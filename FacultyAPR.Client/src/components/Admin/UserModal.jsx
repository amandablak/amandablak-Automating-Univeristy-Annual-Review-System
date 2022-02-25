import React, { useState } from 'react'
import { Button, Modal, ModalHeader, ModalBody } from 'reactstrap'
import UserAddEditForm from './UserAddEditForm'

function UserModalForm(props) {
  const [modal, setModal] = useState(false)

  const toggle = () => {
    setModal(!modal)
  }

  const closeBtn = <button className="close" onClick={toggle}>&times;</button>
  const label = props.buttonLabel

  let button = ''
  let title = ''

  if(label === 'Edit'){
    button = <Button
              color="warning"
              onClick={toggle}>{label}
            </Button>
    title = 'Edit User'
  } else {
    button = <Button className = "mt-2"
              color="info"
              onClick={toggle}>{label}
            </Button>
    title = 'Add New User'
  }


  return (
    <div>
      {button}
      <Modal isOpen={modal} toggle={toggle} className={props.className}>
        <ModalHeader toggle={toggle} close={closeBtn}>{title}</ModalHeader>
        <ModalBody>
          <UserAddEditForm
            addUserToState={props.addUserToState}
            updateState={props.updateState}
            toggle={toggle}
            user={props.user} />
        </ModalBody>
      </Modal>
    </div>
  )
}

export default UserModalForm