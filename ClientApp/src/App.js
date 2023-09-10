import React, { Component } from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';

import { Layout } from './components/Layout';
import { Glossary } from "./components/Glossary";
import './custom.css';

export default class App extends Component {
  static displayName = App.name;

  render() {
      return (
      <Layout>
            <Routes>
                <Route path="/" element={<Glossary />} />
                <Route path="glossary" element={<Glossary />} />

                {/* default redirect to home page */}
                <Route path="*" element={<Navigate to="/" />} />
            </Routes>
      </Layout>
    );
  }
}
