import { CREATE_FORM, UPDATE_REVIEW_SECTION, UPDATE_SECTION, UPDATE_STATE, UPDATE_FACULTY_COMMENT } from "../formActionTypes";


const intial_state = null;

export default function reducer(state = intial_state, action) {
  switch (action.type) {
    case CREATE_FORM: {
        return {...action.payload.form}
    }
    case UPDATE_SECTION: {
      var form = state;
      const {sectionId, groupId, content} = action.payload;
      var group = form.groups.find(group => group.groupId === groupId);
      var section = group.sections.find(section => section.sectionId === sectionId);
      section.content = content;
      return {...state, ...form};
    }
    case UPDATE_FACULTY_COMMENT: {
      var form = state;
      form.spotScoreSection.facultyComment = action.payload.facultyComment;
      return {...state, ...form};
    }
    case UPDATE_REVIEW_SECTION: {
      var form = state;
      const {groupId, content} = action.payload;
      var group = form.groups.find(group => group.groupId === groupId);
      group.comments = content;
      return {...state, ...form};
    }
    case UPDATE_STATE: {
      var form = state;
      form.state = action.payload.state;
      return {...state, ...form}
    }
    default:
      return state
  }
}