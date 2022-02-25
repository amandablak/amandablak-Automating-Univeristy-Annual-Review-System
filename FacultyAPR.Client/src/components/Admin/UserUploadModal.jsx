import {Modal, Button} from 'react-bootstrap'
import {useState} from 'react';
import FileReader from "./UploadUserFile"

const UserUploadModal = ()=>{
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

return (
  <>
    <Button className = "mt-2-user" variant = "info" onClick={handleShow}>
      Upload Users
    </Button>

    <Modal show={show} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Upload a CSV File</Modal.Title>
      </Modal.Header>
      <Modal.Body>
      <FileReader/> 
       </Modal.Body>
    </Modal>
  </>
);
}

export default UserUploadModal