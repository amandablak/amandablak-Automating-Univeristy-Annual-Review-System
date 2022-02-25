import SectionType from "./SectionType"

type Section = {
    sectionTitle: string;
    sectionDescription: string;
    sectionType: SectionType;
    sectionId: string;
    groupId: string;
    options: Array<string>;
    content: string;
    modified: string;
    orderIndex: number;
}

export default Section