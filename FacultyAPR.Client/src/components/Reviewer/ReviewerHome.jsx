import {Tab, Row, Nav, Col, Container} from 'react-bootstrap'
import React, {useState, useEffect} from 'react'
import {GetReviewerFormStubs} from '../../api/Routing'
import {DisplayForms} from '../common/DisplayForms'
import {connect} from 'react-redux'
import {createForm} from '../../redux/form/formActions'
import {CreateNewForm} from '../../common/helpers/form'
import {GetFormStructureByClassification} from '../../api/Routing'
import {useHistory} from 'react-router-dom'
import FormState from '../../models/BusinessObjects/Form/FormState'
import {LoadForm} from '../../common/helpers/form'
import Toast from 'react-bootstrap/Toast'
import Button from 'react-bootstrap/Button'
import './reviewer.css'
 
const ReviewerHome = ({createForm,user, isAuthenticated}) => {
    var history = useHistory();
    if (!isAuthenticated)
    {
        history.push("/Login")
    }
    const [formStubs, setFormStubs] = useState([]);
    useEffect(() => {
        async function getFormStubs() {
            try
            {
                const stubs = await GetReviewerFormStubs(user);
                setFormStubs(stubs.data ?? []);
            } 
            catch(_) {}
        }
        getFormStubs();
        console.log(formStubs);
    }, [])

    var currentYear = "2021";
    var currentYearFormCreated = false;
    for(var i = 0; i < formStubs.length; i++){
        if (formStubs[i].formYear === currentYear){
            currentYearFormCreated = true;
        }
    }
    return (
      <Container>
        <div className = "reviewer-table">
        <Tab.Container defaultActiveKey="ReviewRequired">
        <Row>
            <Col sm={3}>
                <Nav variant="pills" className="flex-column">
                    <Nav.Item>
                        <Nav.Link className = "tab-link" eventKey="ReviewRequired">Review Required</Nav.Link>
                    </Nav.Item>
                    <Nav.Item>
                        <Nav.Link className = "tab-link" eventKey="FacultyAcknowledge">Faculty Acknowledge </Nav.Link>
                    </Nav.Item>
                    <Nav.Item>
                        <Nav.Link className = "tab-link" eventKey="Completed">Completed</Nav.Link>
                    </Nav.Item>
                    <Nav.Item>
                        <Nav.Link className = "tab-link" eventKey="Submitted">Submitted to Dean</Nav.Link>
                    </Nav.Item>
                </Nav>
            </Col>
                <Col sm={9}>
                    <Tab.Content>
                        <Tab.Pane eventKey="ReviewRequired"title="Review Required">
                        <DisplayForms formState="Review" formStubs={formStubs} createForm={createForm} user={user}/>
                        </Tab.Pane>
                        <Tab.Pane eventKey="FacultyAcknowledge" title = "Faculty Acknowledge">
                        <DisplayForms  formState="FacultyAck" formStubs={formStubs} createForm={createForm} user={user}/>
                        </Tab.Pane>
                        <Tab.Pane eventKey="Completed" title = "Completed">
                        <DisplayForms  formState="Completed" formStubs={formStubs} createForm={createForm} user={user}/>
                        </Tab.Pane>
                        <Tab.Pane eventKey="Submitted" title = "Submitted">
                        <DisplayForms  formState="Submitted" formStubs={formStubs} createForm={createForm} user={user}/>
                        </Tab.Pane>
                    </Tab.Content>
                </Col>
        </Row>
              {(formStubs.filter(formStub => formStub.state === "Review").length != 0 && user.userType === "FacultyChair")
                        ?<Notification/>
                        : null
                        }
              {(formStubs.filter(formStub => formStub.state === "Completed").length != 0 && user.userType === "FacultyChair")
                        ?<Notification2/>
                        : null
                        }
              {(formStubs.filter(formStub => formStub.state === "Submitted").length != 0 && user.userType === "Dean")
                        ?<Notification1/>
                        : null
                        }
        </Tab.Container>
        </div>
        </Container>
    );

}

function Notification2() {
  const [showA, setShowA] = useState(true);
  const [showB, setShowB] = useState(true);

  const toggleShowA = () => setShowA(!showA);
  const toggleShowB = () => setShowB(!showB);

  return (
    <Row>
      <Col xs={6}>
        <Toast show={showA} onClose={toggleShowA}>
          <Toast.Header>
            <img
              src="holder.js/20x20?text=%20"
              className="rounded mr-2"
              alt=""
            />
            <strong className="mr-auto">Reminder</strong>
          </Toast.Header>
          <Toast.Body>A form has been signed off by Facuties and it is now under Completed </Toast.Body>
        </Toast>
      </Col>
      <Col xs={6}>
        <Button onClick={toggleShowA}>
          Show Reminder
        </Button>
      </Col>
    </Row>
  );
}

function Notification() {
  const [showA, setShowA] = useState(true);
  const [showB, setShowB] = useState(true);

  const toggleShowA = () => setShowA(!showA);
  const toggleShowB = () => setShowB(!showB);

  return (
    <Row>
      <Col xs={6}>
        <Toast show={showA} onClose={toggleShowA}>
          <Toast.Header>
            <img
              src="holder.js/20x20?text=%20"
              className="rounded"
              alt=""
            />
            <strong className="mr-auto">Reminder</strong>
          </Toast.Header>
          <Toast.Body>Forms are ready to be reviewd</Toast.Body>
        </Toast>
      </Col>
      <Col xs={6}>
        <Button className="mr-2" onClick={toggleShowA}>
          Show Reminder
        </Button>
      </Col>
    </Row>
  );
}


  function Notification1() {
    const [showA, setShowA] = useState(true);
    const [showB, setShowB] = useState(true);
  
    const toggleShowA = () => setShowA(!showA);
    const toggleShowB = () => setShowB(!showB);
  
    return (
      <Row>
        <Col xs={6}>
          <Toast show={showA} onClose={toggleShowA}>
            <Toast.Header>
              <img
                src="holder.js/20x20?text=%20"
                className="rounded mr-2"
                alt=""
              />
              <strong className="mr-auto">Reminder</strong>
            </Toast.Header>
            <Toast.Body>Reminder: a form has been submitted</Toast.Body>
          </Toast>
        </Col>
        <Col xs={6}>
          <Button onClick={toggleShowA}>
            Show Reminder
          </Button>
        </Col>
      </Row>
    );
  }
  
const mapStateToProps = state => {
    const user = state.user;
    return {user: user, isAuthenticated: state.auth.isAuthenticated};
  };
//export default ReviewerHome;
export default connect(mapStateToProps,{createForm})(ReviewerHome)


