import * as types from "./formActionTypes";
import * as actions from "./formActions";
import * as Form from '../../models/BusinessObjects/Form/Form.ts';
import * as FormState from '../../models/BusinessObjects/Form/FormState.ts';



describe("actions", () => {
    it("should create an action to create a form", () => {
        const form = (Form)
        const expectedAction = {
            type: types.CREATE_FORM,
            payload: {form}
        }
        expect(actions.createForm(form)).toEqual(expectedAction)
    })

    it("should create an action to update section", () => {
        const sectionId  = "123"
        const groupId = "234"
        const content = "I did my scholarship requirement"
        const expectedAction = {
            type: types.UPDATE_SECTION,
            payload: {
                sectionId: sectionId,
                groupId: groupId,
                content: content
            }
        }
            expect(actions.updateSection(sectionId, groupId, content)).toEqual(expectedAction)
        
        })

        it("should create an action to update form state", () => {
            
            const form = Form
            const state = (form.state === FormState)
            const expectedAction = {
                type: types.UPDATE_STATE,
                payload: {state}
            }
            expect(actions.update_state(state)).toEqual(expectedAction)
        })
    }) 