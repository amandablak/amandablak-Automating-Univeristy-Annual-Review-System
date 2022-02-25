import React from "react";
import { shallow } from "enzyme";
import Adapter from 'enzyme-adapter-react-16'
import { DisplayForms } from "./DisplayForms";
import { configure } from 'enzyme';

configure({ adapter: new Adapter() });
let wrapper;
beforeEach(() => {
    wrapper = shallow(<DisplayForms/>);
});

describe("DisplayForms", () => {
  it("renders <DisplayForms/> 0 component", () => {
    expect(wrapper.find(DisplayForms)).toHaveLength(0);
  });
  
});