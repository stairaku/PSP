/* 
 * Title Direct Search Services
 *
 * Title Direct Search Services
 *
 * OpenAPI spec version: 4.0.1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace Pims.Ltsa.Models
{
    /// <summary>
    /// Title
    /// </summary>
    [DataContract]
    public partial class Title : IFieldedData
    {
        /// <summary>
        /// Indicates the state with respect to Title lifecycle.
        /// </summary>
        /// <value>Indicates the state with respect to Title lifecycle.</value>
        [JsonStringEnumMemberConverterOptions(deserializationFailureFallbackValue: 0)]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public enum TitleStatusEnum
        {
            Unknown = 0,
            /// <summary>
            /// Enum REGISTERED for value: REGISTERED
            /// </summary>
            [EnumMember(Value = "REGISTERED")]
            REGISTERED = 1,
            /// <summary>
            /// Enum CANCELLED for value: CANCELLED
            /// </summary>
            [EnumMember(Value = "CANCELLED")]
            CANCELLED = 2
        }
        /// <summary>
        /// Indicates the state with respect to Title lifecycle.
        /// </summary>
        /// <value>Indicates the state with respect to Title lifecycle.</value>
        [DataMember(Name = "titleStatus", EmitDefaultValue = false)]
        public TitleStatusEnum TitleStatus { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Title" /> class.
        /// </summary>
        /// <param name="titleStatus">Indicates the state with respect to Title lifecycle. (required).</param>
        /// <param name="titleIdentifier">titleIdentifier (required).</param>
        /// <param name="tombstone">tombstone (required).</param>
        /// <param name="ownershipGroups">ownershipGroups (required).</param>
        /// <param name="taxAuthorities">taxAuthorities (required).</param>
        /// <param name="descriptionsOfLand">descriptionsOfLand (required).</param>
        /// <param name="legalNotationsOnTitle">legalNotationsOnTitle.</param>
        /// <param name="chargesOnTitle">chargesOnTitle.</param>
        /// <param name="duplicateCertificatesOfTitle">duplicateCertificatesOfTitle.</param>
        /// <param name="titleTransfersOrDispositions">titleTransfersOrDispositions.</param>
        /// <param name="correctionsAltos1">correctionsAltos1.</param>
        /// <param name="corrections">corrections.</param>
        public Title(TitleStatusEnum titleStatus = default, TitleIdentifier titleIdentifier = default, TitleTombstone tombstone = default, List<TitleOwnershipGroup> ownershipGroups = default, List<TaxAuthority> taxAuthorities = default, List<DescriptionOfLand> descriptionsOfLand = default, List<LegalNotationOnTitle> legalNotationsOnTitle = default, List<ChargeOnTitle> chargesOnTitle = default, List<DuplicateCertificate> duplicateCertificatesOfTitle = default, List<TitleTransferDisposition> titleTransfersOrDispositions = default, List<Altos1TitleCorrection> correctionsAltos1 = default, List<TitleCorrection> corrections = default)
        {
            // to ensure "titleIdentifier" is required (not null)
            if (titleIdentifier == null)
            {
                throw new InvalidDataException("titleIdentifier is a required property for Title and cannot be null");
            }
            else
            {
                this.TitleIdentifier = titleIdentifier;
            }
            // to ensure "tombstone" is required (not null)
            if (tombstone == null)
            {
                throw new InvalidDataException("tombstone is a required property for Title and cannot be null");
            }
            else
            {
                this.Tombstone = tombstone;
            }
            // to ensure "ownershipGroups" is required (not null)
            if (ownershipGroups == null)
            {
                throw new InvalidDataException("ownershipGroups is a required property for Title and cannot be null");
            }
            else
            {
                this.OwnershipGroups = ownershipGroups;
            }
            // to ensure "taxAuthorities" is required (not null)
            if (taxAuthorities == null)
            {
                throw new InvalidDataException("taxAuthorities is a required property for Title and cannot be null");
            }
            else
            {
                this.TaxAuthorities = taxAuthorities;
            }
            // to ensure "descriptionsOfLand" is required (not null)
            if (descriptionsOfLand == null)
            {
                throw new InvalidDataException("descriptionsOfLand is a required property for Title and cannot be null");
            }
            else
            {
                this.DescriptionsOfLand = descriptionsOfLand;
            }
            this.TitleStatus = titleStatus;
            this.LegalNotationsOnTitle = legalNotationsOnTitle;
            this.ChargesOnTitle = chargesOnTitle;
            this.DuplicateCertificatesOfTitle = duplicateCertificatesOfTitle;
            this.TitleTransfersOrDispositions = titleTransfersOrDispositions;
            this.CorrectionsAltos1 = correctionsAltos1;
            this.Corrections = corrections;
        }


        /// <summary>
        /// Gets or Sets TitleIdentifier
        /// </summary>
        [DataMember(Name = "titleIdentifier", EmitDefaultValue = false)]
        public TitleIdentifier TitleIdentifier { get; set; }

        /// <summary>
        /// Gets or Sets Tombstone
        /// </summary>
        [DataMember(Name = "tombstone", EmitDefaultValue = false)]
        public TitleTombstone Tombstone { get; set; }

        /// <summary>
        /// Gets or Sets OwnershipGroups
        /// </summary>
        [DataMember(Name = "ownershipGroups", EmitDefaultValue = false)]
        public List<TitleOwnershipGroup> OwnershipGroups { get; set; }

        /// <summary>
        /// Gets or Sets TaxAuthorities
        /// </summary>
        [DataMember(Name = "taxAuthorities", EmitDefaultValue = false)]
        public List<TaxAuthority> TaxAuthorities { get; set; }

        /// <summary>
        /// Gets or Sets DescriptionsOfLand
        /// </summary>
        [DataMember(Name = "descriptionsOfLand", EmitDefaultValue = false)]
        public List<DescriptionOfLand> DescriptionsOfLand { get; set; }

        /// <summary>
        /// Gets or Sets LegalNotationsOnTitle
        /// </summary>
        [DataMember(Name = "legalNotationsOnTitle", EmitDefaultValue = false)]
        public List<LegalNotationOnTitle> LegalNotationsOnTitle { get; set; }

        /// <summary>
        /// Gets or Sets ChargesOnTitle
        /// </summary>
        [DataMember(Name = "chargesOnTitle", EmitDefaultValue = false)]
        public List<ChargeOnTitle> ChargesOnTitle { get; set; }

        /// <summary>
        /// Gets or Sets DuplicateCertificatesOfTitle
        /// </summary>
        [DataMember(Name = "duplicateCertificatesOfTitle", EmitDefaultValue = false)]
        public List<DuplicateCertificate> DuplicateCertificatesOfTitle { get; set; }

        /// <summary>
        /// Gets or Sets TitleTransfersOrDispositions
        /// </summary>
        [DataMember(Name = "titleTransfersOrDispositions", EmitDefaultValue = false)]
        public List<TitleTransferDisposition> TitleTransfersOrDispositions { get; set; }

        /// <summary>
        /// Gets or Sets CorrectionsAltos1
        /// </summary>
        [DataMember(Name = "correctionsAltos1", EmitDefaultValue = false)]
        public List<Altos1TitleCorrection> CorrectionsAltos1 { get; set; }

        /// <summary>
        /// Gets or Sets Corrections
        /// </summary>
        [DataMember(Name = "corrections", EmitDefaultValue = false)]
        public List<TitleCorrection> Corrections { get; set; }
    }
}
