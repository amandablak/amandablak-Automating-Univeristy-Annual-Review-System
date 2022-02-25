import Form from 'react-bootstrap/Form'
import {FormTextInput} from './FormTextInput'
import {FormMultiSelectInput} from './FormMultiSelectInput'
import SectionType from '../../models/BusinessObjects/Form/SectionType'
import { FormRadioInput } from './FormRadioInput'

const APRFormSection = ({section, state, onUpdate, user}) => {
    
    const handleContentChange = (id, event) => {
        onUpdate(id, event.target.value);
    }

    const isReadonly = (state != "Draft" && state != "FacultyAck" || user.userType != "Faculty")
    
    const renderSectionByType = () => {
        switch (section.sectionType){
            case SectionType.TEXTBOX:
                return <FormTextInput content={section.content} onChange={event => handleContentChange(section.sectionId, event)} isReadonly={isReadonly}/>
            case SectionType.MULTISELECT:
                return <FormMultiSelectInput content={section.content} onChange={event => handleContentChange(section.sectionId, event)} isReadonly={isReadonly} options={section.options}/>
            case SectionType.RADIO:
                return <FormRadioInput content={section.content} onChange={event => handleContentChange(section.sectionId, event)} isReadonly={isReadonly} options={section.options} id={section.sectionId}/>
            default:
                return
        }
    }

    return (
        <div>
        <Form.Label>{section.sectionTitle}</Form.Label>
        <Form.Text id="Description">{section.sectionDescription}</Form.Text>
        {renderSectionByType()}
        </div>
    )
}

export default APRFormSection;