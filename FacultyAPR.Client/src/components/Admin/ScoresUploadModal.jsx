import {Modal, Button} from 'react-bootstrap'
import {useState} from 'react';
import FileReader from "./UploadSpotScoresFile"

const ScoresUploadModal = ()=>{
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

return (
  <>
    <Button className = "mt-2-user" variant = "info" onClick={handleShow}>
      Upload Spot Scores
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

export default ScoresUploadModal