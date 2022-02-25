import {Modal, Button} from 'react-bootstrap'
import {useState} from 'react';
import SchemaForm from './SchemaForm.jsx'

const CreateForm = ()=>{
  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
 
return (
  <>
    <Button className = "mt-2-user" variant = "info" onClick={handleShow}>
      Create New Form
    </Button>
    <Modal 
    show={show} onHide={handleClose}
      size="lg"
      aria-labelledby="contained-modal-title-vcenter"
      centered>
      <Modal.Header closeButton>
        <Modal.Title>Add the Form Schema and Submit</Modal.Title>
      </Modal.Header>
      <Modal.Body>
      <SchemaForm/>
      </Modal.Body> 
    </Modal>
  </>
);
};

export default CreateForm