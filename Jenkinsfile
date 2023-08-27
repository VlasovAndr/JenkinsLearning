pipeline {
  agent any
  environment {
      DOTNET_SYSTEM_GLOBALIZATION_INVARIANT = 1
  }

    stages {
        stage('Git clone') {
            steps {
                git 'https://github.com/VlasovAndr/JenkinsLearning.git'
            }
        }
        stage('Restore Nuget') {
            steps {
                sh 'dotnet restore'
            }
        }
        stage('Build') {
            steps {
                sh 'dotnet build'
            }
        }

        stage('Test') {
            steps {
                sh 'dotnet test'
            }
        }
        stage('reports') {
            steps {
            script {
                    allure([
                            includeProperties: false,
                            jdk: '',
                            properties: [],
                            reportBuildPolicy: 'ALWAYS',
                            results: [[path: 'JenkinsIntegration/bin/Debug/net6.0/allure-results']]
                    ])
                }
            }
        }

    }
}
