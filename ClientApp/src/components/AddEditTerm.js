import React from 'react';
import { Formik, Field, Form, useField } from 'formik';



const MyTextArea = ({ label, ...props }) => {
    // useField() returns [formik.getFieldProps(), formik.getFieldMeta()]
    // which we can spread on <input> and alse replace ErrorMessage entirely.
    const [field, meta] = useField(props);
    return (
        <>
            <textarea className="text-area" {...field} {...props} />
            {meta.touched && meta.error ? (
                <div className="error">{meta.error}</div>
            ) : null}
        </>
    );
};

export function AddEditTerm(props) {
    let sendSave = props.onSubmit;
    let showForm = props.onCancel;
    let formError = props.formError;

    let termId = props.hasOwnProperty('termId') ? props.termId : 0;


    return (
        <div>
            <h3>{termId === 0 ? 'Create New Term' : 'Edit Term' }</h3>
            <Formik
                initialValues={{
                    id: termId,
                    term: props.term,
                    definition: props.definition
                }}
                onSubmit={(values, { actions, resetForm }) => {
                    sendSave(values);
                }}

                
            >
               
                {({ isSubmitting }) => (
                    <Form>
                        <div className="row ">
                            <div className="col-lg-12 text-center">
                                <span className="error-msg">{formError.title}</span>
                            </div>
                        </div>
                        <div className="row ">
                            <div className="col-lg-2 pull-left">
                                <label htmlFor="term">Term</label>
                            </div>
                            <div className="col-lg-6 pull-left">
                                <label htmlFor="definition">Definition</label>
                            </div>
                            <div className="col-lg-4"></div>
                        </div>
                        <div className="row">
                            <div className="col-lg-2 pull-left">
                                <Field id="term" name="term" placeholder="A word or phrase" />
                            </div>
                            <div className="col-lg-6 pull-left">
                                <MyTextArea
                                    label="Definition"
                                    id="definition"
                                    name="definition"
                                    rows="3"
                                    placeholder="A description of the meaning of the term."
                                />
                            </div>
                            <div className="col-lg-4 pull-left ">
                                <button type="submit" className="btn btn-primary btn-sm formButton" >Submit</button>
                                <button className="btn btn-secondary btn-sm formButton" onClick={(e) => { showForm(false); e.preventDefault(); }}>Cancel</button>
                            </div>
                        </div>
                        <div className="row ">
                            <div className="col-lg-2 pull-left">
                                <span className="error-msg">{formError.errors.Term}</span>
                            </div>
                            <div className="col-lg-6 pull-left">
                                <span className="error-msg">{formError.errors.Definition}</span>
                            </div>
                            <div className="col-lg-4"></div>
                        </div>

                    </Form>
                )}
            </Formik>
        </div>
    )
}

