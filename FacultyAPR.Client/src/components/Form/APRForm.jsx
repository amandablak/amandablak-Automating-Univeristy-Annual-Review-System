import React from 'react'
import {APRFormGroup} from './APRFormGroup'
import FormComponent from 'react-bootstrap/Form'
import Container from 'react-bootstrap/Container'
import { connect } from "react-redux";
import {updateSection, updateReviewSection, updateFacultyComment} from '../../redux/form/formActions'
import {Button} from 'react-bootstrap'
import {update_state} from '../../redux/form/formActions'
import {SavedFormContent} from '../../common/helpers/form'
import {useHistory} from 'react-router-dom'
import {ReviewForm, isFormComplete, AckFacReview, ReadytoSubmit2Dean, FacultyConcurButton, Submitted} from '../../common/helpers/form'
import {APRFormSpotScoresComponent} from './APRFormSpotScoresComponent'
import LeavePageConfirmation from '../../common/LeavePageConfirmation.jsx'
import "./form.css"

const APRForm = ({form, updateSection, updateReviewSection, updateFacultyComment, user, isAuthenticated}) => {
    var history = useHistory();
    if (!isAuthenticated)
    {
        history.push("/Login")
    }
    if (form === null)
    {
        history.push("/Forbidden")
        return <div></div>
    }
    LeavePageConfirmation(isFormComplete(form))
    return (
        <div className = "form">
        <Container>
        <FormComponent>
            { (form.spotScoreSection.scores == null ||form.spotScoreSection.scores.length == 0 )
                ? null
                : <APRFormSpotScoresComponent
                    updateFacultyComment={(comment) => updateFacultyComment(comment.target.value)}
                    spotScoreData={form.spotScoreSection}
                    state={form.state} user={user}/>
            }
            {form.groups.sort((a, b) => a.orderIndex - b.orderIndex).map((group) => {
                return <APRFormGroup
                    key = {group.orderIndex} 
                    group={group}
                    state={form.state}
                    user={user}
                    onSectionChange={(sectionId, content) => updateSection(sectionId, group.groupId, content)}
                    onReviewSectionChange={updateReviewSection}/>
                })
            }           
        </FormComponent>
        {(form.state == "Draft" || form.state == "FacultyAck") && user.userType === "Faculty"  && isFormComplete(form)
            ?
            <Button className="mt-2 ml-2 no-print" variant="info" size="lg" onClick={
                () => {
                    reviewFormOnClick(update_state, form)
                        .then(() => {history.push("/")});
                    }}>
            Submit for Review
            </Button>
            : null
        }
        
        {form.state == "Draft"
        ?
        <Button className="mt-2 ml-2 no-print" variant="info" size="lg" onClick={
                () => {
                    updateFormContentOnClick(update_state, form)
                        .then(() => {history.push("/")});}}>
            Save Draft
        </Button>
        :null
        }

        {form.state == "Review" && (user.userType === "FacultyChair" || user.userType === "Dean")
        ?<Button className="mt-2 ml-2 no-print" variant="info" size="lg" onClick={
            () => {
                reviewFormOnClick(update_state, form)
                    .then(() => {history.push("/")});}}>
                Save Comments
        </Button>
        :null
        }
        
        {form.state == "Review" && isFormComplete(form) && (user.userType === "FacultyChair" || user.userType === "Dean")
        ?
        <Button className="mt-2 ml-2 no-print" variant="info" size="lg" onClick={
            () => {
                AckFacReviewButtonOnClick(update_state, form)
                    .then(() => {history.push("/")});}}>
                Acknowledge Faculty
        </Button>
        : null
        }

        {form.state == "Completed" && user.userType == "FacultyChair" 
        ?
        <form>
        <div className="radio">
        <label> 
          <input type="radio" value="option1"checked={true}/>
          I, as a faculty, concur to Chair's comments
        </label>
        </div>

        <div className="radio">
        <label> 
          <input type="radio" value="option1"/>
          I, as a faculty Chair, approves this form
        </label>
        </div>
        </form>
        : null
          }


        {form.state == "Submitted"
        ?
        <form>
        <div className="radio">
        <label> 
          <input type="radio" value="option1" checked={true}/>
          I, as a faculty, concur to Chair's comments
        </label>
        </div>

        <div className="radio">
        <label> 
          <input type="radio" value="option1" checked={true}/>
          I, as a faculty Chair, approves this form
        </label>
        </div>
        </form>
        : null
          } 

        

        {form.state == "Completed" && user.userType == "FacultyChair" 
        ?
        <Button className="mt-2 ml-2 no-print" variant="info" size="lg" onClick={
            () => {
                SubmittedOneClick(update_state, form)
                    .then(() => {history.push("/")});}}>
                Submit form to Dean
         </Button>
         :null
        }  

        {(form.state == "Review") && user.userType === "FacultyChair"  && isFormComplete(form)
            ?
            <Button className="mt-2 ml-2 no-print" variant="info" size="lg" onClick={
                () => {
                    FacultyConcurButtonOnClink(update_state, form)
                        .then(() => {history.push("/")});
                    }}>
            Ask Faculty to Sign 
            </Button>
            : null
            
        } 

        {(form.state == "ToBeSigned") && user.userType === "Faculty"  && isFormComplete(form)
        ?
        <form>
        <div className="radio">
        <label>
            <input type="radio" value="option1"/>
            I, as a faculty, concur to Chair's comments (If you do not concur with the Chair, please contact Chair or Dean directly)     
        </label>
        </div>
        </form>

        : null
        } 

        {(form.state == "ToBeSigned") && user.userType === "Faculty"  && isFormComplete(form)
            ?
            <Button className="mt-2 ml-2 no-print" variant="info" size="lg" onClick={
                () => {
                    ReadytoSubmit2DeanOnClick(update_state, form)
                        .then(() => {history.push("/")});
                    }}>
            Submit form to Chair
            </Button>
            : null
        } 
 

        <Button className="mt-2 ml-2 no-print" variant="info" size="lg" onClick={() => {window.print();}}>Print</Button>
        </Container>
        </div>
    )
    
}

const SubmittedOneClick = async (update_state, form)=> {
    try
    {
        return Submitted(form, update_state);
    }
    catch(_)
    {
        console.log("An error occured in the backend");
    }
}

const FacultyConcurButtonOnClink = async (update_state, form)=> {
    
    try
    {
        return FacultyConcurButton(form, update_state);
    }
    catch(_)
    {
        console.log("An error occured in the backend");
    }
}
const ReadytoSubmit2DeanOnClick = async (update_state, form)=> {
    try
    {
        return ReadytoSubmit2Dean(form, update_state);
    }
    catch(_)
    {
        console.log("An error occured in the backend");
    }
}

const AckFacReviewButtonOnClick = async (update_state, form)=> {
    try
    {
        return AckFacReview(form, update_state);
    }
    catch(_)
    {
        console.log("An error occured in the backend");
    }
}

const updateFormContentOnClick = async (update_state, form) => {
    try
    {
        console.log("Update click", form)
        return SavedFormContent(form, update_state);
    }
    catch(_)
    {
        console.log("An error occured in the backend");
    }
}

const reviewFormOnClick = async (update_state, form) => {
    try
    {
        return ReviewForm(form, update_state);
    }
    catch(_)
    {
        console.log("An error occured in the backend");
    }
}

const mapStateToProps = state => {
    const form = state.form;
    const user = state.user;
    return {form: form, user: user, isAuthenticated: state.auth.isAuthenticated};
  };

export default connect(mapStateToProps, {updateSection, updateReviewSection, updateFacultyComment})(APRForm);