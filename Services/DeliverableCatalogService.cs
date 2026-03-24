using PointScore.Models.DTOs;

namespace PointScore.Services
{
    public class DeliverableCatalogService : IDeliverableCatalogService
    {
        private readonly List<DeliverableCatalogDto> _deliverablesCatalog;

        public DeliverableCatalogService()
        {
            _deliverablesCatalog = InitializeCatalog();
        }

        private List<DeliverableCatalogDto> InitializeCatalog()
        {
            var catalog = new List<DeliverableCatalogDto>
            {
                new DeliverableCatalogDto { Id = 1, DIDNumber = "DI-SESS-81830", Description = "As Built Configuration List – Common (ABCL-C)", Type = "CDRL", WhenNeeded = "At DD250", NeededForWSM = "Y", FunctionalArea = "CM", Complexity = 6, ScheduleScore = 3, TotalScore = 4.2m, IsApplicableWSM = true, ApplicableWSMScore = 4.2m },
                new DeliverableCatalogDto { Id = 2, DIDNumber = "DI-SESS-81022", Description = "Functional or Physical Configuration Audit (FCA/PCA) Report", Type = "CDRL", WhenNeeded = "Initial run", NeededForWSM = "N", FunctionalArea = "CM", Complexity = 7, ScheduleScore = 4, TotalScore = 5.2m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 3, DIDNumber = "DI-SESS-80640", Description = "Request for Variance (RFV)", Type = "CDRL", WhenNeeded = "by block", NeededForWSM = "Y", FunctionalArea = "CM", Complexity = 1, ScheduleScore = 1, TotalScore = 1, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 4, DIDNumber = "DI-MGMT-81453", Description = "Data Accession List", Type = "CDRL", WhenNeeded = "as requested by the USG", NeededForWSM = "N", FunctionalArea = "CM", Complexity = 1, ScheduleScore = 2, TotalScore = 1.6m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 5, DIDNumber = "DI-SESS-80858", Description = "Supplier's Configuration Management Plan", Type = "NON-CDRL", WhenNeeded = "once", NeededForWSM = "N", FunctionalArea = "CM", Complexity = 3, ScheduleScore = 4, TotalScore = 3.6m, IsApplicableWSM = true, ApplicableWSMScore = 3.6m },
                new DeliverableCatalogDto { Id = 6, DIDNumber = "DI-SESS-80642", Description = "Notice of Revision (NOR)", Type = "NON-CDRL", WhenNeeded = "by block", NeededForWSM = "Y", FunctionalArea = "CM", Complexity = 1, ScheduleScore = 0.5m, TotalScore = 0.7m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 7, DIDNumber = "DI-SESS-81253", Description = "Configuration Status Accounting (CSA) Information", Type = "NON-CDRL", WhenNeeded = "as requested by the USG", NeededForWSM = "Y", FunctionalArea = "CM", Complexity = 3, ScheduleScore = 1, TotalScore = 1.8m, IsApplicableWSM = true, ApplicableWSMScore = 1.8m },
                new DeliverableCatalogDto { Id = 8, DIDNumber = "DI-SESS-81646", Description = "Configuration Audit Plan", Type = "NON-CDRL", WhenNeeded = "as requested by the USG", NeededForWSM = "Y", FunctionalArea = "CM", Complexity = 2, ScheduleScore = 2, TotalScore = 2, IsApplicableWSM = true, ApplicableWSMScore = 2 },
                new DeliverableCatalogDto { Id = 9, DIDNumber = "DI-MGMT-81844", Description = "Information Assurance (IA) Test Plan", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Cyber", Complexity = 7, ScheduleScore = 3, TotalScore = 4.6m, IsApplicableWSM = true, ApplicableWSMScore = 4.6m },
                new DeliverableCatalogDto { Id = 10, DIDNumber = "OPT SEL WKSHT", Description = "Technical Data Package (Models, Level 3 Documentation)", Type = "CDRL", WhenNeeded = "Attachment to SOW", NeededForWSM = "", FunctionalArea = "Systems Engineering", Complexity = 9, ScheduleScore = 7, TotalScore = 7.8m, IsApplicableWSM = true, ApplicableWSMScore = 7.8m },
                new DeliverableCatalogDto { Id = 11, DIDNumber = "DI-IPSC-81434", Description = "Interface Requirements Specification", Type = "CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Systems Engineering", Complexity = 9, ScheduleScore = 4, TotalScore = 6, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 12, DIDNumber = "DI-EDRS-82290", Description = "DOORS", Type = "CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Systems Engineering", Complexity = 8, ScheduleScore = 3, TotalScore = 5, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 13, DIDNumber = "DI-SDMP-81465", Description = "Performance Specification Document", Type = "CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Systems Engineering", Complexity = 6, ScheduleScore = 2, TotalScore = 3.6m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 14, DIDNumber = "DI-SESS-81008", Description = "Special Tooling (ST) Engineering Design Data and Associated Lists", Type = "CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Systems Engineering", Complexity = 2, ScheduleScore = 1, TotalScore = 1.4m, IsApplicableWSM = true, ApplicableWSMScore = 1.4m },
                new DeliverableCatalogDto { Id = 15, DIDNumber = "DI-SESS-81000", Description = "Product Engineering Design Data and Associated Lists", Type = "CDRL", WhenNeeded = "Technical Data Package", NeededForWSM = "", FunctionalArea = "Systems Engineering", Complexity = 7.5m, ScheduleScore = 5, TotalScore = 6, IsApplicableWSM = true, ApplicableWSMScore = 6 },
                new DeliverableCatalogDto { Id = 16, DIDNumber = "DI-DRPR-81961", Description = "Engineering Drawing Tree", Type = "CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Systems Engineering", Complexity = 5, ScheduleScore = 1, TotalScore = 2.6m, IsApplicableWSM = true, ApplicableWSMScore = 2.6m },
                new DeliverableCatalogDto { Id = 17, DIDNumber = "DI-MGMT-82133", Description = "Requirements Traceability Verification Matrix (RTVM)", Type = "CDRL", WhenNeeded = "as requested by the USG", NeededForWSM = "", FunctionalArea = "Systems Engineering", Complexity = 7, ScheduleScore = 3, TotalScore = 4.6m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 18, DIDNumber = "DI-MGMT-81808", Description = "Contractor's Risk Management Plan", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Systems Engineering", Complexity = 5, ScheduleScore = 3, TotalScore = 3.8m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 19, DIDNumber = "DI-MGMT-81809", Description = "Risk Registry", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Systems Engineering", Complexity = 3.5m, ScheduleScore = 1, TotalScore = 2, IsApplicableWSM = true, ApplicableWSMScore = 2 },
                new DeliverableCatalogDto { Id = 20, DIDNumber = "DI-SESS-82411", Description = "Acquisition & Sustainment Data Package (ASDP) Interface Control Document (ICD)", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Systems Engineering", Complexity = 5, ScheduleScore = 2.5m, TotalScore = 3.5m, IsApplicableWSM = true, ApplicableWSMScore = 3.5m },
                new DeliverableCatalogDto { Id = 21, DIDNumber = "DI-MISC 80759", Description = "Acceptance Test Equipment (ATE) Validation Plan", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Systems Engineering", Complexity = 6, ScheduleScore = 1.5m, TotalScore = 3.3m, IsApplicableWSM = true, ApplicableWSMScore = 3.3m },
                new DeliverableCatalogDto { Id = 22, DIDNumber = "DI-MNTY-81188", Description = "Verification, Demonstration, and Evaluation Plan", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Systems Engineering", Complexity = 7, ScheduleScore = 3, TotalScore = 4.6m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 23, DIDNumber = "DI-MISC-80508", Description = "Technical Report/ Study/Services", Type = "NON-CDRL", WhenNeeded = "Special Studies Report", NeededForWSM = "", FunctionalArea = "Systems Engineering", Complexity = 6, ScheduleScore = 2, TotalScore = 3.6m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 24, DIDNumber = "DI-SESS-81497", Description = "Reliability and Maintainability Predictions and Allocations Report", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Systems Engineering", Complexity = 7, ScheduleScore = 4, TotalScore = 5.2m, IsApplicableWSM = true, ApplicableWSMScore = 5.2m },
                new DeliverableCatalogDto { Id = 25, DIDNumber = "DI-MISC-80508", Description = "Technical Report/ Study/Services", Type = "NON-CDRL", WhenNeeded = "WSM Assessments", NeededForWSM = "", FunctionalArea = "Systems Engineering", Complexity = 7, ScheduleScore = 3, TotalScore = 4.6m, IsApplicableWSM = true, ApplicableWSMScore = 4.6m },
                new DeliverableCatalogDto { Id = 26, DIDNumber = "DI-SESS-80639", Description = "Engineering Change Proposal (ECP)", Type = "NON-CDRL", WhenNeeded = "as requested by the USG", NeededForWSM = "", FunctionalArea = "Systems Engineering", Complexity = 6, ScheduleScore = 2.5m, TotalScore = 3.9m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 27, DIDNumber = "DI-MFFP-81402", Description = "Finish Specification", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Systems Engineering", Complexity = 2, ScheduleScore = 1, TotalScore = 1.4m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 28, DIDNumber = "DI-MGMT-81605", Description = "Briefing Material", Type = "NON-CDRL", WhenNeeded = "Technical Interchange Meeting (TIM) Briefings", NeededForWSM = "", FunctionalArea = "Systems Engineering", Complexity = 4, ScheduleScore = 2, TotalScore = 2.8m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 29, DIDNumber = "DI-MISC-80508", Description = "Technical Report/ Study/Services", Type = "NON-CDRL", WhenNeeded = "Safety Support Report", NeededForWSM = "", FunctionalArea = "Systems Engineering/Safety", Complexity = 5, ScheduleScore = 4, TotalScore = 4.4m, IsApplicableWSM = true, ApplicableWSMScore = 4.4m },
                new DeliverableCatalogDto { Id = 30, DIDNumber = "DI-MGMT-82274", Description = "DMSMS Life cycle Mgmt Data", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Systems Engineering/Logistics", Complexity = 4.5m, ScheduleScore = 2, TotalScore = 3, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 31, DIDNumber = "DI-MFFP-81403", Description = "Corrosion Prevention and Control Plan (CPCP)", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Systems Engineering/Logistics", Complexity = 2.5m, ScheduleScore = 2, TotalScore = 2.2m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 32, DIDNumber = "DI-STDZ-81993", Description = "Parts, Materials, and Processed (PM&P) Management Plan", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Systems Engineering/Logistics", Complexity = 2, ScheduleScore = 2, TotalScore = 2, IsApplicableWSM = true, ApplicableWSMScore = 2 },
                new DeliverableCatalogDto { Id = 33, DIDNumber = "DI-PACK-81059", Description = "Performance Oriented Packaging (POP) Test Report", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Systems Engineering/Logistics", Complexity = 2, ScheduleScore = 2, TotalScore = 2, IsApplicableWSM = true, ApplicableWSMScore = 2 },
                new DeliverableCatalogDto { Id = 34, DIDNumber = "DI-MISC-80508", Description = "Quality Records", Type = "NON-CDRL", WhenNeeded = "Records", NeededForWSM = "", FunctionalArea = "Systems Engineering/Logistics", Complexity = 2, ScheduleScore = 2, TotalScore = 2, IsApplicableWSM = true, ApplicableWSMScore = 2 },
                new DeliverableCatalogDto { Id = 35, DIDNumber = "DI-SESS-81004", Description = "Special Inspection Equipment (SIE) Engineering Design Data and Associated Lists (including STE), SIE Equipment Descriptive Documentation, Calibration Procedures", Type = "CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Systems Engineering/Quality/Logistics", Complexity = 3, ScheduleScore = 2, TotalScore = 2.4m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 36, DIDNumber = "DI-MGMT-82256", Description = "Supply Chain Risk Management Plan", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Systems Engineering/Quality/Logistics", Complexity = 3.5m, ScheduleScore = 3.5m, TotalScore = 3.5m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 37, DIDNumber = "DI-MISC-80508", Description = "Technical Report/ Study/Services", Type = "CDRL", WhenNeeded = "Technical and Supply Bulletins as reqested by the USG", NeededForWSM = "", FunctionalArea = "Logistics", Complexity = 2, ScheduleScore = 2, TotalScore = 2, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 38, DIDNumber = "DI-MGMT-80441", Description = "Government Property (GP) Inventory Report", Type = "CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Logistics", Complexity = 1, ScheduleScore = 2.5m, TotalScore = 1.9m, IsApplicableWSM = true, ApplicableWSMScore = 1.9m },
                new DeliverableCatalogDto { Id = 39, DIDNumber = "DI-PACK- 80121", Description = "Special Packaging Instructions (SPI)", Type = "CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Logistics", Complexity = 1, ScheduleScore = 1, TotalScore = 1, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 40, DIDNumber = "DI-MGMT-82163", Description = "Maintenance and Repair Parts Data Report", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Logistics", Complexity = 2, ScheduleScore = 2, TotalScore = 2, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 41, DIDNumber = "DI-PSSS-81872", Description = "Level of Repair Analysis (LORA)", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Logistics", Complexity = 3, ScheduleScore = 3, TotalScore = 3, IsApplicableWSM = true, ApplicableWSMScore = 3 },
                new DeliverableCatalogDto { Id = 42, DIDNumber = "DI-MISC-80508", Description = "Technical Report/ Study/Services", Type = "NON-CDRL", WhenNeeded = "Revisions to the publications' development/delivery schedule", NeededForWSM = "", FunctionalArea = "Logistics", Complexity = 3, ScheduleScore = 3, TotalScore = 3, IsApplicableWSM = true, ApplicableWSMScore = 3 },
                new DeliverableCatalogDto { Id = 43, DIDNumber = "DI-TMSS-81818", Description = "Technical Manual Validation Plan", Type = "NON-CDRL", WhenNeeded = "by block", NeededForWSM = "", FunctionalArea = "Logistics", Complexity = 2, ScheduleScore = 4, TotalScore = 3.2m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 44, DIDNumber = "DI-TMSS- 81819", Description = "Technical Manual Validation Certificate", Type = "NON-CDRL", WhenNeeded = "by block", NeededForWSM = "", FunctionalArea = "Logistics", Complexity = 1, ScheduleScore = 1, TotalScore = 1, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 45, DIDNumber = "DI-RMSS-81821", Description = "Certificate of Verification Comment incorporation", Type = "NON-CDRL", WhenNeeded = "by block", NeededForWSM = "", FunctionalArea = "Logistics", Complexity = 1, ScheduleScore = 1, TotalScore = 1, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 46, DIDNumber = "DI-MISC-80508", Description = "Technical Report/ Study/Services", Type = "NON-CDRL", WhenNeeded = "Technical Publications", NeededForWSM = "", FunctionalArea = "Logistics", Complexity = 3.5m, ScheduleScore = 6, TotalScore = 5, IsApplicableWSM = true, ApplicableWSMScore = 5 },
                new DeliverableCatalogDto { Id = 47, DIDNumber = "DI-MISC-80508", Description = "Technical Report/ Study/Services", Type = "NON-CDRL", WhenNeeded = "Packing Data List (PDL)", NeededForWSM = "", FunctionalArea = "Logistics", Complexity = 2, ScheduleScore = 2, TotalScore = 2, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 48, DIDNumber = "DI-MISC-81499", Description = "Packaging Kit Contents List", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Logistics", Complexity = 2, ScheduleScore = 2, TotalScore = 2, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 49, DIDNumber = "DI-MISC-81397", Description = "Hazardous Materials Management Program (HMMP) Report", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Logistics", Complexity = 2, ScheduleScore = 2, TotalScore = 2, IsApplicableWSM = true, ApplicableWSMScore = 2 },
                new DeliverableCatalogDto { Id = 50, DIDNumber = "DI-FNCL-80331A", Description = "Quarterly Technical and Business Status Reports (8.1)", Type = "CDRL", WhenNeeded = "Monthly", NeededForWSM = "", FunctionalArea = "Program Management/Finance", Complexity = 5, ScheduleScore = 0.5m, TotalScore = 2.3m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 51, DIDNumber = "DI-MGMT-81468", Description = "Contract Funds Status Report (CFSR)", Type = "CDRL", WhenNeeded = "Monthly", NeededForWSM = "", FunctionalArea = "Program Management/Finance", Complexity = 5, ScheduleScore = 1, TotalScore = 2.6m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 52, DIDNumber = "DI-NDTI-82326", Description = "Product Acceptance Report (PAR)", Type = "CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Quality", Complexity = 2, ScheduleScore = 1, TotalScore = 1.4m, IsApplicableWSM = true, ApplicableWSMScore = 1.4m },
                new DeliverableCatalogDto { Id = 53, DIDNumber = "DI-SESS-80789", Description = "Quality Assurance Provisions (QAP)", Type = "CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Quality", Complexity = 3.5m, ScheduleScore = 3, TotalScore = 3.2m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 54, DIDNumber = "DI-MISC-80508", Description = "Technical Report/ Study/Services", Type = "NON-CDRL", WhenNeeded = "Fault Tree Analysis", NeededForWSM = "", FunctionalArea = "Quality", Complexity = 2, ScheduleScore = 2, TotalScore = 2, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 55, DIDNumber = "DI-MISC-80508", Description = "Technical Report/ Study/Services", Type = "NON-CDRL", WhenNeeded = "Key Processes and Verification Requirements", NeededForWSM = "", FunctionalArea = "Quality", Complexity = 1.5m, ScheduleScore = 2, TotalScore = 1.8m, IsApplicableWSM = true, ApplicableWSMScore = 1.8m },
                new DeliverableCatalogDto { Id = 56, DIDNumber = "DI-SAFT-80101", Description = "System Safety Hazard Analysis Report (SSHAR)", Type = "NON-CDRL", WhenNeeded = "System Requirements Hazard Analysis (SRHA)", NeededForWSM = "", FunctionalArea = "Safety", Complexity = 4, ScheduleScore = 4, TotalScore = 4, IsApplicableWSM = true, ApplicableWSMScore = 4 },
                new DeliverableCatalogDto { Id = 57, DIDNumber = "DI-SAFT-81841", Description = "Operating, Support, Health, and Safety (OSH&S) Hazard Analysis Report", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Safety", Complexity = 4, ScheduleScore = 4, TotalScore = 4, IsApplicableWSM = true, ApplicableWSMScore = 4 },
                new DeliverableCatalogDto { Id = 58, DIDNumber = "DI SAFT-80103", Description = "Engineering Change Proposal System Safety Report (ECPSSR)", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Safety", Complexity = 4, ScheduleScore = 3, TotalScore = 3.4m, IsApplicableWSM = true, ApplicableWSMScore = 3.4m },
                new DeliverableCatalogDto { Id = 59, DIDNumber = "DI-SAFT-80104", Description = "Waiver or Deviation System Safety Report (WDSSR)", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Safety", Complexity = 3.5m, ScheduleScore = 2, TotalScore = 2.6m, IsApplicableWSM = true, ApplicableWSMScore = 2.6m },
                new DeliverableCatalogDto { Id = 60, DIDNumber = "DI-MISC-80508", Description = "Technical Report/ Study/Services", Type = "NON-CDRL", WhenNeeded = "Safety Impact Report", NeededForWSM = "", FunctionalArea = "Safety", Complexity = 3.5m, ScheduleScore = 3.5m, TotalScore = 3.5m, IsApplicableWSM = true, ApplicableWSMScore = 3.5m },
                new DeliverableCatalogDto { Id = 61, DIDNumber = "DI-SAFT-80102", Description = "Safety Assessment Report (SAR)", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Safety", Complexity = 4, ScheduleScore = 4, TotalScore = 4, IsApplicableWSM = true, ApplicableWSMScore = 4 },
                new DeliverableCatalogDto { Id = 62, DIDNumber = "DI-MGMT-81580", Description = "Contractor's Standard Operating Procedures", Type = "NON-CDRL", WhenNeeded = "Single Delivery", NeededForWSM = "", FunctionalArea = "Security", Complexity = 3, ScheduleScore = 2, TotalScore = 2.4m, IsApplicableWSM = true, ApplicableWSMScore = 2.4m },
                new DeliverableCatalogDto { Id = 63, DIDNumber = "DI-ADMN-81306", Description = "Program Protection Implementation Plan (PPIP)", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Security", Complexity = 5, ScheduleScore = 6, TotalScore = 5.6m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 64, DIDNumber = "DI-IPSC-81435", Description = "Software Description Documents (SDD)", Type = "CDRL", WhenNeeded = "tailored by WSM", NeededForWSM = "", FunctionalArea = "Software", Complexity = 5, ScheduleScore = 2, TotalScore = 3.2m, IsApplicableWSM = true, ApplicableWSMScore = 3.2m },
                new DeliverableCatalogDto { Id = 65, DIDNumber = "DI-IPSC-81433", Description = "Software Requirements Specification (SRS)", Type = "CDRL", WhenNeeded = "tailored by WSM", NeededForWSM = "", FunctionalArea = "Software", Complexity = 5, ScheduleScore = 2, TotalScore = 3.2m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 66, DIDNumber = "DI-IPSC-81436", Description = "Interface Design Description", Type = "CDRL", WhenNeeded = "Software Interface Design Description", NeededForWSM = "", FunctionalArea = "Software", Complexity = 7, ScheduleScore = 3, TotalScore = 4.6m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 67, DIDNumber = "DI-IPSC-81442", Description = "Software Version Description Document", Type = "CDRL", WhenNeeded = "SW", NeededForWSM = "", FunctionalArea = "Software", Complexity = 6, ScheduleScore = 3, TotalScore = 4.2m, IsApplicableWSM = true, ApplicableWSMScore = 4.2m },
                new DeliverableCatalogDto { Id = 68, DIDNumber = "DI-IPSC-81444", Description = "Computer Program Operator's Manual (CPOM)", Type = "CDRL", WhenNeeded = "SW", NeededForWSM = "", FunctionalArea = "Software", Complexity = 1, ScheduleScore = 1, TotalScore = 1, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 69, DIDNumber = "DI-MGMT-82035", Description = "Software: Development Report and Maintenance Report", Type = "NON-CDRL", WhenNeeded = "SW", NeededForWSM = "", FunctionalArea = "Software", Complexity = 5, ScheduleScore = 2, TotalScore = 3.2m, IsApplicableWSM = true, ApplicableWSMScore = 3.2m },
                new DeliverableCatalogDto { Id = 70, DIDNumber = "DI-MISC-80508", Description = "Technical Report/ Study/Services", Type = "NON-CDRL", WhenNeeded = "SW/Firmware Installation Procedures", NeededForWSM = "", FunctionalArea = "Software", Complexity = 6.5m, ScheduleScore = 3, TotalScore = 4.4m, IsApplicableWSM = true, ApplicableWSMScore = 4.4m },
                new DeliverableCatalogDto { Id = 71, DIDNumber = "DI-MISC-80508", Description = "Technical Report - Study/Services", Type = "NON-CDRL", WhenNeeded = "Software Integrated Test Document", NeededForWSM = "", FunctionalArea = "Software", Complexity = 6, ScheduleScore = 3, TotalScore = 4.2m, IsApplicableWSM = true, ApplicableWSMScore = 4.2m },
                new DeliverableCatalogDto { Id = 72, DIDNumber = "DI-AVCS-80700", Description = "Computer Software Product End Items", Type = "NON-CDRL", WhenNeeded = "tailored by WSM", NeededForWSM = "", FunctionalArea = "Software", Complexity = 7, ScheduleScore = 2, TotalScore = 4, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 73, DIDNumber = "DI-SESS-81770", Description = "System/Software Integration Laboratory (SIL) Development and Management Plan", Type = "NON-CDRL", WhenNeeded = "SW", NeededForWSM = "", FunctionalArea = "Software", Complexity = 6, ScheduleScore = 3, TotalScore = 4.2m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 74, DIDNumber = "DI-IPSC-82367", Description = "Hardware, Software and Firmware Utilization Matrix (HSFUM)", Type = "NON-CDRL", WhenNeeded = "as requested by the USG", NeededForWSM = "", FunctionalArea = "Software", Complexity = 5, ScheduleScore = 3, TotalScore = 3.8m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 75, DIDNumber = "DI-MISC-80508", Description = "Technical Report - Study/Services", Type = "NON-CDRL", WhenNeeded = "Test Readiness Review (TRR) Brieifing Package", NeededForWSM = "", FunctionalArea = "Test", Complexity = 3, ScheduleScore = 1, TotalScore = 1.8m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 76, DIDNumber = "DI-NDTI-80566", Description = "Test Plans/Test Procedures", Type = "NON-CDRL", WhenNeeded = "Test Plan", NeededForWSM = "", FunctionalArea = "Test", Complexity = 5, ScheduleScore = 3, TotalScore = 3.8m, IsApplicableWSM = false, ApplicableWSMScore = 0 },
                new DeliverableCatalogDto { Id = 77, DIDNumber = "DI-NDTI-80603", Description = "Test Procedure", Type = "NON-CDRL", WhenNeeded = null, NeededForWSM = "", FunctionalArea = "Test", Complexity = 5, ScheduleScore = 4, TotalScore = 4.4m, IsApplicableWSM = true, ApplicableWSMScore = 4.4m },
                new DeliverableCatalogDto { Id = 78, DIDNumber = "DI-NDTI-80809", Description = "Test/Inspection Report", Type = "NON-CDRL", WhenNeeded = "Test and Analysis Report", NeededForWSM = "", FunctionalArea = "Test", Complexity = 6, ScheduleScore = 4, TotalScore = 4.8m, IsApplicableWSM = true, ApplicableWSMScore = 4.8m }
            };
            
            return catalog;
        }

        /// <summary>
        /// Enriquece los deliverables con la información completa de FunctionalAreas desde la BD
        /// Maneja casos donde FunctionalArea contiene múltiples áreas separadas por "/"
        /// </summary>
        private IEnumerable<DeliverableCatalogDto> EnrichWithFunctionalAreas(
            IEnumerable<DeliverableCatalogDto> deliverables,
            IEnumerable<FunctionalAreaDto>? functionalAreas)
        {
            if (functionalAreas == null || !functionalAreas.Any())
                return deliverables;

            // Crear diccionario para búsqueda rápida (case-insensitive)
            var faDict = functionalAreas.ToDictionary(
                fa => fa.Name.Trim(),
                fa => fa,
                StringComparer.OrdinalIgnoreCase);

            foreach (var deliverable in deliverables)
            {
                if (!string.IsNullOrWhiteSpace(deliverable.FunctionalArea))
                {
                    // Dividir por "/" en caso de múltiples áreas funcionales
                    var areaNames = deliverable.FunctionalArea
                        .Split('/', StringSplitOptions.RemoveEmptyEntries)
                        .Select(name => name.Trim())
                        .ToList();

                    deliverable.FunctionalAreas = new List<FunctionalAreaDto>();

                    foreach (var areaName in areaNames)
                    {
                        if (faDict.TryGetValue(areaName, out var fullFa))
                        {
                            deliverable.FunctionalAreas.Add(fullFa);
                        }
                    }
                }
            }

            return deliverables;
        }

        public IEnumerable<DeliverableCatalogDto> GetAllDeliverables(IEnumerable<FunctionalAreaDto>? functionalAreas = null)
        {
            return EnrichWithFunctionalAreas(_deliverablesCatalog, functionalAreas);
        }

        public IEnumerable<DeliverableCatalogDto> GetFilteredDeliverables(DeliverableCatalogFilterDto filter, IEnumerable<FunctionalAreaDto>? functionalAreas = null)
        {
            var query = _deliverablesCatalog.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(filter.Type))
            {
                query = query.Where(d => d.Type.Equals(filter.Type, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filter.FunctionalArea))
            {
                query = query.Where(d => d.FunctionalArea?.Contains(filter.FunctionalArea, StringComparison.OrdinalIgnoreCase) ?? false);
            }

            if (!string.IsNullOrWhiteSpace(filter.NeededForWSM))
            {
                query = query.Where(d => d.NeededForWSM.Equals(filter.NeededForWSM, StringComparison.OrdinalIgnoreCase));
            }

            if (filter.IsApplicableWSM.HasValue)
            {
                query = query.Where(d => d.IsApplicableWSM == filter.IsApplicableWSM.Value);
            }

            if (filter.MinComplexity.HasValue)
            {
                query = query.Where(d => d.Complexity >= filter.MinComplexity.Value);
            }

            if (filter.MaxComplexity.HasValue)
            {
                query = query.Where(d => d.Complexity <= filter.MaxComplexity.Value);
            }

            if (filter.MinScore.HasValue)
            {
                query = query.Where(d => d.TotalScore >= filter.MinScore.Value);
            }

            if (filter.MaxScore.HasValue)
            {
                query = query.Where(d => d.TotalScore <= filter.MaxScore.Value);
            }

            var result = query.OrderBy(d => d.DIDNumber);
            return EnrichWithFunctionalAreas(result, functionalAreas);
        }

        public DeliverableCatalogDto? GetDeliverableById(int id, IEnumerable<FunctionalAreaDto>? functionalAreas = null)
        {
            var deliverable = _deliverablesCatalog
                .FirstOrDefault(d => d.Id == id);

            if (deliverable != null && functionalAreas != null && functionalAreas.Any())
            {
                var enriched = EnrichWithFunctionalAreas(new[] { deliverable }, functionalAreas);
                return enriched.FirstOrDefault();
            }

            return deliverable;
        }

        public IEnumerable<string> GetFunctionalAreas()
        {
            return _deliverablesCatalog
                .Where(d => !string.IsNullOrWhiteSpace(d.FunctionalArea))
                .Select(d => d.FunctionalArea!)
                .Distinct()
                .OrderBy(fa => fa);
        }

        public IEnumerable<DeliverableCatalogDto> GetDeliverablesByFunctionalArea(string functionalArea, IEnumerable<FunctionalAreaDto>? functionalAreas = null)
        {
            var result = _deliverablesCatalog
                .Where(d => d.FunctionalArea?.Equals(functionalArea, StringComparison.OrdinalIgnoreCase) ?? false)
                .OrderBy(d => d.DIDNumber);

            return EnrichWithFunctionalAreas(result, functionalAreas);
        }

        public IEnumerable<DeliverableCatalogDto> GetApplicableWSMDeliverables(IEnumerable<FunctionalAreaDto>? functionalAreas = null)
        {
            var result = _deliverablesCatalog
                .Where(d => d.IsApplicableWSM)
                .OrderByDescending(d => d.ApplicableWSMScore)
                .ThenBy(d => d.DIDNumber);

            return EnrichWithFunctionalAreas(result, functionalAreas);
        }

        public object GetCatalogStatistics(IEnumerable<FunctionalAreaDto>? functionalAreas = null)
        {
            var enrichedDeliverables = EnrichWithFunctionalAreas(_deliverablesCatalog, functionalAreas).ToList();

            var totalDeliverables = enrichedDeliverables.Count;
            var cdrlCount = enrichedDeliverables.Count(d => d.Type == "CDRL");
            var nonCdrlCount = enrichedDeliverables.Count(d => d.Type == "NON-CDRL");
            var applicableWSMCount = enrichedDeliverables.Count(d => d.IsApplicableWSM);
            var neededForWSMYes = enrichedDeliverables.Count(d => d.NeededForWSM == "Y");
            var neededForWSMNo = enrichedDeliverables.Count(d => d.NeededForWSM == "N");

            var functionalAreaStats = enrichedDeliverables
                .Where(d => !string.IsNullOrWhiteSpace(d.FunctionalArea))
                .GroupBy(d => d.FunctionalArea)
                .Select(g => new
                {
                    FunctionalAreaName = g.Key,
                    FunctionalAreas = g.First().FunctionalAreas, // Incluir objetos completos si están disponibles
                    Count = g.Count(),
                    ApplicableWSM = g.Count(d => d.IsApplicableWSM),
                    AverageComplexity = g.Average(d => d.Complexity),
                    AverageTotalScore = g.Average(d => d.TotalScore)
                })
                .OrderByDescending(s => s.Count);

            return new
            {
                TotalDeliverables = totalDeliverables,
                CDRLCount = cdrlCount,
                NonCDRLCount = nonCdrlCount,
                ApplicableWSMCount = applicableWSMCount,
                NeededForWSM = new
                {
                    Yes = neededForWSMYes,
                    No = neededForWSMNo
                },
                AverageComplexity = enrichedDeliverables.Average(d => d.Complexity),
                AverageTotalScore = enrichedDeliverables.Average(d => d.TotalScore),
                AverageApplicableWSMScore = enrichedDeliverables.Where(d => d.IsApplicableWSM).Average(d => d.ApplicableWSMScore),
                FunctionalAreaStatistics = functionalAreaStats
            };
        }

        public IEnumerable<DeliverableCatalogDto> GetDeliverablesByRole(string roleName, IEnumerable<FunctionalAreaDto>? functionalAreas = null)
        {
            if (functionalAreas == null || !functionalAreas.Any())
            {
                // Si no hay información de áreas funcionales, retornar colección vacía
                return Enumerable.Empty<DeliverableCatalogDto>();
            }

            // Filtrar las áreas funcionales que tienen el rol especificado como DefaultRoleName
            var matchingFunctionalAreaNames = functionalAreas
                .Where(fa => !string.IsNullOrWhiteSpace(fa.DefaultRoleName) && 
                            fa.DefaultRoleName.Equals(roleName, StringComparison.OrdinalIgnoreCase))
                .Select(fa => fa.Name)
                .ToList();

            if (!matchingFunctionalAreaNames.Any())
            {
                // Si no hay áreas funcionales con ese rol, retornar colección vacía
                return Enumerable.Empty<DeliverableCatalogDto>();
            }

            // Filtrar deliverables que pertenezcan a las áreas funcionales encontradas
            // Considerar que un deliverable puede tener múltiples áreas separadas por "/"
            var result = _deliverablesCatalog
                .Where(d => !string.IsNullOrWhiteSpace(d.FunctionalArea) &&
                           matchingFunctionalAreaNames.Any(faName =>
                               d.FunctionalArea.Split('/', StringSplitOptions.RemoveEmptyEntries)
                                   .Select(name => name.Trim())
                                   .Any(name => name.Equals(faName, StringComparison.OrdinalIgnoreCase))))
                .OrderBy(d => d.FunctionalArea)
                .ThenBy(d => d.DIDNumber);

            return EnrichWithFunctionalAreas(result, functionalAreas);
        }

        public IEnumerable<DeliverableCatalogDto> GetDeliverablesByRoles(IEnumerable<string> roleNames, IEnumerable<FunctionalAreaDto>? functionalAreas = null)
        {
            if (roleNames == null || !roleNames.Any())
            {
                return Enumerable.Empty<DeliverableCatalogDto>();
            }

            if (functionalAreas == null || !functionalAreas.Any())
            {
                // Si no hay información de áreas funcionales, solo podemos filtrar por "CM" directamente
                // si está en la lista de roles
                var cmOnly = roleNames.Any(r => r.Equals("CM", StringComparison.OrdinalIgnoreCase));
                if (cmOnly)
                {
                    var result = _deliverablesCatalog
                        .Where(d => !string.IsNullOrWhiteSpace(d.FunctionalArea) &&
                                   d.FunctionalArea.Split('/', StringSplitOptions.RemoveEmptyEntries)
                                       .Select(name => name.Trim())
                                       .Any(name => name.Equals("CM", StringComparison.OrdinalIgnoreCase)))
                        .OrderBy(d => d.FunctionalArea)
                        .ThenBy(d => d.DIDNumber);
                    
                    return EnrichWithFunctionalAreas(result, functionalAreas);
                }
                return Enumerable.Empty<DeliverableCatalogDto>();
            }

            var matchingFunctionalAreaNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var roleName in roleNames)
            {
                // Caso especial: "CM" filtra directamente por FunctionalArea
                if (roleName.Equals("CM", StringComparison.OrdinalIgnoreCase))
                {
                    matchingFunctionalAreaNames.Add("CM");
                }
                else
                {
                    // Filtrar áreas funcionales por DefaultRoleName
                    var areasForRole = functionalAreas
                        .Where(fa => !string.IsNullOrWhiteSpace(fa.DefaultRoleName) && 
                                    fa.DefaultRoleName.Equals(roleName, StringComparison.OrdinalIgnoreCase))
                        .Select(fa => fa.Name);
                    
                    foreach (var area in areasForRole)
                    {
                        matchingFunctionalAreaNames.Add(area);
                    }
                }
            }

            if (!matchingFunctionalAreaNames.Any())
            {
                return Enumerable.Empty<DeliverableCatalogDto>();
            }

            // Filtrar deliverables que pertenezcan a las áreas funcionales encontradas
            var deliverablesResult = _deliverablesCatalog
                .Where(d => !string.IsNullOrWhiteSpace(d.FunctionalArea) &&
                           matchingFunctionalAreaNames.Any(faName =>
                               d.FunctionalArea.Split('/', StringSplitOptions.RemoveEmptyEntries)
                                   .Select(name => name.Trim())
                                   .Any(name => name.Equals(faName, StringComparison.OrdinalIgnoreCase))))
                .OrderBy(d => d.FunctionalArea)
                .ThenBy(d => d.DIDNumber);

            return EnrichWithFunctionalAreas(deliverablesResult, functionalAreas);
        }
    }
}
