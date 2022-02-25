import Form from '../../models/BusinessObjects/Form/Form'
import Rank from '../../models/BusinessObjects/Rank'
import UserType from '../../models/Users/UserType'
import FormState from '../../models/BusinessObjects/Form/FormState'
import SectionType from '../../models/BusinessObjects/Form/SectionType'
import Department from '../../models/BusinessObjects/Department';
import IServerFormStructure from '../types/IServerFormStructure'
import IServerFormContent from '../types/IServerFormContent'
import APRReviewer from '../../models/Users/APRReviewer'
import FacultyUser from '../../models/Users/FacultyUser'
import SectionStructure from '../../models/BusinessObjects/Form/SectionStructure'
import { FacultyRank } from '../types/FacultyRank'
import IServerGroupStructure from '../types/IServerGroupStructure'
import SectionContent from '../../models/BusinessObjects/Form/SectionContent'
import { CreateFormContent } from '../../api/Routing'
import { UpdateFormContent } from '../../api/Routing'
import {GetFormStructureById, GetFormContentById} from '../../api/Routing'
import Group from '../../models/BusinessObjects/Form/Group'
import Section from '../../models/BusinessObjects/Form/Section'

export const CreateFacultyUser = () => {
    var user: FacultyUser = {
        rank: Rank.Professor,
            department: Department.ComputerScience,
            id: "69267c94-e01a-471d-bb39-cdcf84c30e5a",
            firstName: "Quinn",
            lastName: "Wass",
            emailAddress: "wassq@seattleu.edu",
            userType: UserType.Faculty
    }
    return user;
}

export const CreateAPRReviewer = () => {
    var reviewer: APRReviewer = {
        department: Department.ComputerScience,
        userType: UserType.FacultyChair,
        id: "bdd1f0f0-e3c9-4637-9432-ef3293133765",
        firstName: "Eric",
        lastName: "Larson",
        emailAddress: "elarson@seattleu.edu"
    }
    return reviewer;
}

export const CreateSampleForm = () => {
    const form: Form = {
        formId: "78dcd9ea-d08b-449b-aa99-206da2f7e849",
        facultyId: "6dcb8856-b9ce-458b-8a71-5c326a809ce7",
        reviewerId: "cfec7ed3-1c5e-4d08-8865-316a3a0df0ef",
        spotScoreSection: {
            scores: [
                {
                    question: "The course as a whole was well-organized",
                    course: "CPSC3500",
                    percentRespondents: 78.5,
                    meanValue: 4.5
                },
                {
                    question: "The instructor's use of class time was effective",
                    course: "CPSC3500",
                    percentRespondents: 78.5,
                    meanValue: 2.5
                },
                {
                    question: "The course as a whole was well-organized",
                    course: "CPSC4100",
                    percentRespondents: 30.1,
                    meanValue: 3.9
                }
            ],
            facultyComment: "",
            review: ""
        },
        groups: [
            {
                groupId: "e6ec44fb-7ebb-4d9e-9d15-39e2e1b317b0",
                title: "Hobbies",
                description: "All about hobbies",
                orderIndex: 1,
                sections: [
                    {
                        sectionTitle: "Tennis",
                        sectionDescription: "All about tennis",
                        sectionType: SectionType.TEXTBOX,
                        sectionId: "492eae87-7444-432d-910b-c7a05363d162",
                        groupId: "e6ec44fb-7ebb-4d9e-9d15-39e2e1b317b0",
                        content: "I like tennis!",
                        modified: Date.now().toString(),
                        orderIndex: 1,
                        options: []
                    },
                    {
                        sectionTitle: "Do you have more than one hobby?",
                        sectionDescription: "Well? Do ya?",
                        sectionType: SectionType.RADIO,
                        sectionId: "08b08c6f-9764-459d-8c94-12d42a11574b",
                        groupId: "e6ec44fb-7ebb-4d9e-9d15-39e2e1b317b0",
                        content: "Select something!",
                        modified: Date.now().toString(),
                        orderIndex: 2,
                        options: [
                            "Oh yeah, I got LOTS of hobbies",
                            "Hobbies? Yeah, got a few",
                            "I only drink lemon sours",
                            "Hablablabla todo los dias"
                        ]
                    },
                    {
                        sectionTitle: "On a scale of 1 to yes, how much do you enjoy your hobbies?",
                        sectionDescription: "",
                        sectionType: SectionType.RADIO,
                        sectionId: "08b08c6f-9764-459d-8c94-12d42a11574d",
                        groupId: "e6ec44fb-7ebb-4d9e-9d15-39e2e1b317b0",
                        content: "",
                        modified: Date.now().toString(),
                        orderIndex: 3,
                        options: [
                            "1",
                            "eh",
                            "9",
                            "yes"
                        ]
                    },
                    {
                        sectionTitle: "On a scale of 1 to yes, how much do you enjoy your hobbies?",
                        sectionDescription: "",
                        sectionType: SectionType.RADIO,
                        sectionId: "18b08c6f-9764-459d-8c94-12d42a11574d",
                        groupId: "e6ec44fb-7ebb-4d9e-9d15-39e2e1b317b0",
                        content: "",
                        modified: Date.now().toString(),
                        orderIndex: 4,
                        options: [
                            "1",
                            "eh",
                            "9",
                            "yes"
                        ]
                    }
                ],
                comments: ""
            },
            {
                groupId: "8d74bab6-e34b-4cd0-8953-a757a4011460",
                title: "Self Reflection",
                description: "End of Year Self Evaluation",
                orderIndex: 2,
                sections: [
                    {
                        sectionTitle: "What is changed since last year?",
                        sectionDescription: "Reflect on your teaching style, communicating skills.",
                        sectionType: SectionType.TEXTBOX,
                        sectionId: "784ca90d-1c74-4008-b940-2159541dc2cf",
                        groupId: "8d74bab6-e34b-4cd0-8953-a757a4011460",
                        content: "A lot have changed!",
                        modified: Date.now().toString(),
                        orderIndex: 1,
                        options: []
                    },
                    {
                        sectionTitle: "Favorite Class?",
                        sectionDescription: "Think of which class was your favorite and why",
                        sectionType: SectionType.TEXTBOX,
                        sectionId: "00db8008-7ee7-4af6-8352-9fd7ebeccc86",
                        groupId: "8d74bab6-e34b-4cd0-8953-a757a4011460",
                        content: "CPSC 3500!",
                        modified: Date.now().toString(),
                        orderIndex: 2,
                        options: []
                    },
                    {
                        sectionTitle: "Least Favorite Class?",
                        sectionDescription: "Think of which class was your Least favorite and how you can improve it",
                        sectionType: SectionType.TEXTBOX,
                        sectionId: "c6671d4f-f0cd-4c13-bf76-26599b209c26",
                        groupId: "8d74bab6-e34b-4cd0-8953-a757a4011460",
                        content: "CPSC 1000!",
                        modified: Date.now().toString(),
                        orderIndex: 3,
                        options: []
                    }
                ],
                comments: ""
            },
            {
                groupId: "5a9df605-ec22-48c4-b40e-6e10d0d95b42",
                title: "Skills",
                description: "All about skills",
                orderIndex: 3,
                sections: [
                    {
                        sectionTitle: "Math",
                        sectionDescription: "All about math",
                        sectionType: SectionType.TEXTBOX,
                        sectionId: "b532c45c-00ae-42f2-9c85-820ca73b8829",
                        groupId: "5a9df605-ec22-48c4-b40e-6e10d0d95b42",
                        content: "I like math!",
                        modified: Date.now().toString(),
                        orderIndex: 1,
                        options: []
                    }
                ],
                comments: ""
            }
        ],
        comment: "",
        state: FormState.Draft,
        year: "2020",
        rank: FacultyRank.Professor
    }
    return form;
}

export const CombineStructureAndContent = (structure: IServerFormStructure, content: IServerFormContent, reviewerId: string, userId: string) => {
    var form: Form = 
    {
        formId: content.formId,
        facultyId: userId,
        reviewerId: reviewerId,
        groups: structure.groups.map((group) => {
            return {
                groupId: group.groupId,
                title: group.title,
                description: group.description,
                comments: content.reviewContent.filter((review) => review.groupId === group.groupId)[0].comments,
                orderIndex: group.orderIndex,
                sections: group.sections.map((section: SectionStructure) => {
                    var _content = "";
                    content.facultyContent.forEach((_section) =>
                    {
                        if(_section.id === section.sectionId)
                        {
                            _content = _section.content;
                        }
                    })
                    return {
                            content: _content,
                            ...section
                    }
                }),
            }
        }),
        comment: content.formLevelComment,
        state: content.state,
        year: structure.formYear,
        rank: structure.rank,
        spotScoreSection: content.scores
    }
    return form;
}

export const CreateNewForm = async (reviewerId: string, facultyId: string,  createForm: Function, structure: IServerFormStructure) => {
    var content = CreateEmptyFormContentFromStructure(structure, reviewerId, facultyId);
    var form = CombineStructureAndContent(structure, content, reviewerId, facultyId);
    createForm(form);
    var resp = await CreateFormContent(content, facultyId);
    if (resp.status !== 200)
    {
        throw resp;
    }
}

export const LoadForm = async (facultyId: string, reviewerId: string, formId: string) => {
    var structure = await (await GetFormStructureById(formId)).data as IServerFormStructure;
    var content = (await GetFormContentById(formId, facultyId)).data as IServerFormContent;
    var form = CombineStructureAndContent(structure, content, reviewerId, facultyId)
    return form;
}

export const GetFormStructure = (form: Form) => {
    var groups: Array<IServerGroupStructure> = form.groups.map((group) => {
        var sections: Array<SectionStructure> = group.sections.filter((section) => section.groupId === group.groupId).map((section) => {
            return {
                sectionTitle: section.sectionTitle,
                sectionDescription: section.sectionDescription,
                sectionType: section.sectionType,
                sectionId: section.sectionId,
                groupId: section.groupId,
                options: section.options,
                modified: section.modified,
                orderIndex: section.orderIndex
            }
        });
        return {
            groupId: group.groupId,
            title: group.title,
            description: group.description,
            orderIndex: group.orderIndex,
            sections: sections,
            
        }
    });
    var structure: IServerFormStructure = {
        formId: form.formId,
        formYear: form.year,
        groups: groups,
        rank: form.rank
    }

    return structure;
}

export const GetFormContent = (form: Form) => {
    var content: IServerFormContent = {
        formId: form.formId,
        facultyId: form.facultyId,
        reviewerId: form.reviewerId,
        formLevelComment: form.comment,
        state: form.state,
        scores: form.spotScoreSection,
        reviewContent: form.groups.map((group) => {
            return {
                groupId: group.groupId,
                comments: group.comments
            }
        }),
        facultyContent: form.groups.map((group) => {
            var sections: Array<SectionContent> = group.sections.map((section) => {
                return {
                    id: section.sectionId,
                    content: section.content
                }
            })
            return sections;
        }).flat()
    }

    return content;
}

export const CreateEmptyFormContentFromStructure = (structure: IServerFormStructure, reviewerId: string, userId: string) => {
    var content: IServerFormContent = {
        formId: structure.formId,
        facultyId: userId,
        reviewerId: reviewerId,
        formLevelComment: "",
        state: FormState.Draft,
        reviewContent: structure.groups.map((group) => {
            return {
                groupId: group.groupId,
                comments: ""
            }
        }),
        scores: {
            scores: [],
            facultyComment: "",
            review: ""
        }, 
        facultyContent: structure.groups.map((group) => {
            var sections: Array<SectionContent> = group.sections.map((section) => {
                return {
                    id: section.sectionId,
                    content: ""
                }
            })
            return sections;
        }).flat()
    }

    return content;
}

export const SavedFormContent = (form: Form, update_state: Function) => {
    var content = GetFormContent(form);
    var formID = form.formId;
    var facultyID = form.facultyId;
    content.state = FormState.Draft;
    update_state(FormState.Draft);
    UpdateFormContent(formID, facultyID, content)
    .catch(_ => {
        console.log("An error occured in the backend");
    })
}

export const ReviewForm = async (form: Form, update_state: Function) => {
    var content = GetFormContent(form);
    var formID = form.formId;
    var facultyID = form.facultyId;
    content.state = FormState.Review;
    update_state(FormState.Review);
    return UpdateFormContent(formID, facultyID, content).catch(e => {
        console.log("An error occured in the backend");
    })
}

export const isFormComplete = (form: Form) => {
    var emptySections = form.groups.flatMap((group: Group) => {
        return group.sections.flatMap((section: Section) => {
            return section.content === ""
            ? true
            : false;
        })
    })
    .filter((item: boolean) => item === true)
    .length === 0;
    return emptySections;
}

export const AckFacReview = (form: Form, update_state: Function) => {
    var content = GetFormContent(form);
    var formID = form.formId;
    var facultyID = form.facultyId;
    content.state = FormState.FacultyAck;
    update_state(FormState.FacultyAck);
    UpdateFormContent(formID, facultyID, content).catch(e => {
        console.log("An error occured in the backend");
    })
}

export const FacultyConcurButton = (form: Form, update_state: Function) => {
    console.log(FormState.ToBeSigned);
    var content = GetFormContent(form);
    var formID = form.formId;
    var facultyID = form.facultyId;
    content.state = FormState.ToBeSigned;
    update_state(FormState.ToBeSigned);
    UpdateFormContent(formID, facultyID, content).catch(e => {
        console.log("An error occured in the backend");
    })
}

export const ReadytoSubmit2Dean = (form: Form, update_state: Function) => {
    var content = GetFormContent(form);
    var formID = form.formId;
    var facultyID = form.facultyId;
    content.state = FormState.Completed;
    update_state(FormState.Completed);
    UpdateFormContent(formID, facultyID, content).catch(e => {
        console.log("An error occured in the backend");
    })
}

export const Submitted = (form: Form, update_state: Function) => {
    console.log(FormState);
    var content = GetFormContent(form);
    var formID = form.formId;
    var facultyID = form.facultyId;
    content.state = FormState.Submitted;
    update_state(FormState.Submitted);
    UpdateFormContent(formID, facultyID, content).catch(e => {
        console.log("An error occured in the backend");
    })
}